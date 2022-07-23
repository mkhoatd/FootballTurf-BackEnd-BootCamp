using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Persistence;
using WebApi.Persistence;
using WebApi.Repository.Implementation;
using WebApi.Test.Common;
using Xunit;

namespace WebApi.Repository.Test
{
    public class UserRepositoryTest : IDisposable
    {
        #region Seeding
        public UserRepositoryTest()
        {
            ContextOptions = new DbContextOptionsBuilder<AppFootballTurfDbContext>().UseInMemoryDatabase(databaseName: "database_user").Options;
            Context = new AppFootballTurfDbContext(ContextOptions);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
            Seed();
        }
        #endregion

        public DbContextOptions<AppFootballTurfDbContext> ContextOptions { get; }

        public AppFootballTurfDbContext Context { get; set; }

        private void Seed()
        {
            var one = new Game
            {
                Id = CommonTest.IdGameTest1,
                Name = CommonTest.NameGameTest1,
                Status = GameStatus.Starting,
                CodeRoom = CommonTest.CodeGameTest1,
                Updated = DateTime.UtcNow,
                Created = DateTime.UtcNow,
            };
            var two = new Game
            {
                Id = CommonTest.IdGameTest2,
                Name = CommonTest.NameGameTest2,
                Status = GameStatus.NotStarted,
                CodeRoom = CommonTest.CodeGameTest2,
                Updated = DateTime.UtcNow,
                Created = DateTime.UtcNow,
            };

            var userOne = new User
            {
                Id = CommonTest.UserIdTest1,
                Name = CommonTest.UserNameTest1,
                ConnectionId = CommonTest.UserConnectionIdTest1,                
            };

            var userTwo = new User
            {
                Id = CommonTest.UserIdTest2,
                Name = CommonTest.UserNameTest2,
                ConnectionId = CommonTest.UserConnectionIdTest2,
            };

            var userThree = new User
            {
                Id = CommonTest.UserIdTest3,
                Name = CommonTest.UserNameTest3,
                ConnectionId = CommonTest.UserConnectionIdTest2,
            };

            var userFour = new User
            {
                Id = CommonTest.UserIdTest4,
                Name = CommonTest.UserNameTest4,
                ConnectionId = CommonTest.UserConnectionIdTest2,
            };

            var gameUserOne = new GameUser
            {
                GameId = CommonTest.IdGameTest1,
                SourceUserId = CommonTest.UserIdTest1,
                Status = CommonTest.PlayerActive,
                Score = CommonTest.ScoreUserOne
            };

            var gameUserTwo = new GameUser
            {
                GameId = CommonTest.IdGameTest1,
                SourceUserId = CommonTest.UserIdTest2,
                Status = CommonTest.PlayerActive,
                Score = CommonTest.ScoreUserTwo
            };

            var gameUserThree = new GameUser
            {
                GameId = CommonTest.IdGameTest1,
                SourceUserId = CommonTest.UserIdTest3,
                Status = CommonTest.PlayerActive,
                Score = CommonTest.ScoreUserThree
            };

            var gameUserFour = new GameUser
            {
                GameId = CommonTest.IdGameTest1,
                SourceUserId = CommonTest.UserIdTest4,
                Status = CommonTest.PlayerActive,
                Score = CommonTest.ScoreUserFour
            };

            var question = new Question
            {
                Id = CommonTest.QuestionIdTest,
                Weight = CommonTest.QuestionWeight,
                Content = CommonTest.QuestionContentTest,
                GameId = CommonTest.UserGameIdTest1,
            };

            var answer = new Answer
            {
                Id = CommonTest.AnswerIdTest,
                IsCorrect = false,
                Content = CommonTest.AnswerContentTest,
                QuestionId = CommonTest.QuestionIdTest,
                UserId = CommonTest.UserIdTest1,
            };

            var reactHeart = new UserReaction
            {
                SourceUserId = CommonTest.UserIdTest2,
                ReactedAnswerId = CommonTest.AnswerIdTest,
                ReactionTypeId = ReactionType.Heart,
            };

            var reactLike = new UserReaction
            {
                SourceUserId = CommonTest.UserIdTest3,
                ReactedAnswerId = CommonTest.AnswerIdTest,
                ReactionTypeId = ReactionType.Like,
            };

            var reactSad = new UserReaction
            {
                SourceUserId = CommonTest.UserIdTest4,
                ReactedAnswerId = CommonTest.AnswerIdTest,
                ReactionTypeId = ReactionType.Sad,
            };

            Context.AddRange(one, two, userOne, userTwo, userThree, userFour, gameUserOne, gameUserTwo, gameUserThree, gameUserFour,
                question, answer, reactHeart, reactLike, reactSad);
            Context.SaveChanges();
        }
        static Mock<UserManager<TIDentityUser>> GetUserManagerMock<TIDentityUser>() where TIDentityUser : IdentityUser
        {
            return new Mock<UserManager<TIDentityUser>>(
                    new Mock<IUserStore<TIDentityUser>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<TIDentityUser>>().Object,
                    Array.Empty<IUserValidator<TIDentityUser>>(),
                    Array.Empty<IPasswordValidator<TIDentityUser>>(),
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<TIDentityUser>>>().Object);
        }

        [Fact]
        public void Check_User_By_Id_Game_Room()
        {
            var userManagerMock = GetUserManagerMock<User>();
            var repositoryReactionsMock = new Mock<IReactionsRepository>();
            var repositoryUser = new UserRepository(Context,userManagerMock.Object, repositoryReactionsMock.Object);
            string displayName = CommonTest.NameUserTest;
            string idGame = CommonTest.UserGameIdTest1;
            var tag = repositoryUser.CheckUserByGameIdAndDisplayNameAsync(displayName, idGame).Result;
            Assert.False(tag);
        }

        [Fact]
        public void Can_Get_User_By_Room()
        {
            var userManagerMock = GetUserManagerMock<User>();
            var repositoryReactionsMock = new Mock<IReactionsRepository>();
            var repositoryUser = new UserRepository(Context, userManagerMock.Object, repositoryReactionsMock.Object);
            var tag = repositoryUser.GetUserByGameAsync(CommonTest.IdGameTest1).Result;
            int quantityUserByGame = 4;
            Assert.True(quantityUserByGame == tag.Count);
        }

        [Fact]
        public void Can_Get_User_By_Id()
        {
            var userManagerMock = GetUserManagerMock<User>();
            var repositoryReactionsMock = new Mock<IReactionsRepository>();
            var repositoryUser = new UserRepository(Context, userManagerMock.Object, repositoryReactionsMock.Object);
            var userCurrent = repositoryUser.GetUserByIdAsync(CommonTest.UserIdTest1).Result;
            Assert.NotNull(userCurrent);
        }

        [Fact]
        public void Can_Get_List_User_Rank_Point_By_Game_Id()
        {
            var userManagerMock = GetUserManagerMock<User>();
            var repositoryReactionsMock = new Mock<IReactionsRepository>();
            var repositoryUser = new UserRepository(Context, userManagerMock.Object, repositoryReactionsMock.Object);
            var listUserRank = repositoryUser.GetUsersRankPointByGameIdAsync(CommonTest.IdGameTest1).Result;
            var quantityUserRank = 4;
            Assert.Equal(quantityUserRank, listUserRank.Count);
        }

        [Fact]
        public void Can_Get_User_Rank_Current_By_Id_User_And_Game_Id()
        {
            IList<string> ids = new List<string>
            {
                CommonApp.PlayerRole
            };
            var userManagerMock = GetUserManagerMock<User>();
            userManagerMock.Setup(u => u.GetRolesAsync(It.IsAny<User>())).Returns(Task.FromResult(ids));
            var repositoryReactionsMock = new Mock<IReactionsRepository>();
            repositoryReactionsMock.Setup(u => u.GetQuantityReactHeart(It.IsAny<string>())).Returns(Task.FromResult(0));
            repositoryReactionsMock.Setup(u => u.GetQuantityReactSad(It.IsAny<string>())).Returns(Task.FromResult(0));
            repositoryReactionsMock.Setup(u => u.GetQuantityReactLike(It.IsAny<string>())).Returns(Task.FromResult(0));

            var repositoryUser = new UserRepository(Context, userManagerMock.Object, repositoryReactionsMock.Object);
            var rankUserCurrent = repositoryUser.GetUserRankCurrentByIdUserAndGameId(CommonTest.UserIdTest1,CommonTest.IdGameTest1, CommonTest.QuestionIdTest).Result;
            int userRankPointExpect = 4;
            int userRankReactExpect = 4;
            Assert.Equal(rankUserCurrent.RankUserPoint, userRankPointExpect);
            Assert.Equal(rankUserCurrent.RankUserReact, userRankReactExpect);
        }

        [Fact]
        public void Can_Get_List_User_React()
        {
            IList<string> ids = new List<string>
            {
                CommonApp.PlayerRole
            };

            var userManagerMock = GetUserManagerMock<User>();
            userManagerMock.Setup(u => u.GetRolesAsync(It.IsAny<User>())).Returns(Task.FromResult(ids));
            var repositoryReactionsMock = new Mock<IReactionsRepository>();
            var repositoryUser = new UserRepository(Context, userManagerMock.Object, repositoryReactionsMock.Object);
            List<ReturnAppUser> users = new List<ReturnAppUser>();
            List<UserReact> userReacts = new List<UserReact>();
            var listUserReact = repositoryUser.GetListUserReact(users, userReacts).Result;
            Assert.NotNull(listUserReact);
        }

        [Fact]
        public void Can_Get_Host_Game()
        {
            IList<string> ids = new List<string>
            {
                CommonApp.HostRole
            };
            var userManagerMock = GetUserManagerMock<User>();
            userManagerMock.Setup(u => u.GetRolesAsync(It.IsAny<User>())).Returns(Task.FromResult(ids));

            var repositoryReactionsMock = new Mock<IReactionsRepository>();
            var repositoryUser = new UserRepository(Context, userManagerMock.Object, repositoryReactionsMock.Object);
            var hostGame = repositoryUser.GetHostGame(CommonTest.IdGameTest1).Result;
            Assert.NotNull(hostGame);
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
