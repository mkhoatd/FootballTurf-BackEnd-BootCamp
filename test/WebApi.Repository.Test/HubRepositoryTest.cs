using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Domain.Enum;
using WebApi.Persistence;
using WebApi.Repository.DTOs;
using WebApi.Repository.Implementation;
using WebApi.Repository.Interface;
using WebApi.Test.Common;
using Xunit;

namespace WebApi.Repository.Test
{
    public class HubRepositoryTest : IDisposable
    {

        #region Seeding
        public HubRepositoryTest()
        {
            ContextOptions = new DbContextOptionsBuilder<AppFootballTurfDbContext>().UseInMemoryDatabase(databaseName: "database_hub").Options;
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

            var gameUserOne = new GameUser
            {
                GameId = CommonTest.IdGameTest1,
                SourceUserId = CommonTest.UserIdTest1,
                Status = CommonTest.PlayerActive,
                Score = 0
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

            Context.AddRange(one, two, userOne, userTwo, gameUserOne, question, answer);
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
        public void Check_Update_ConnectionId_User()
        {
            var userManagerMock = GetUserManagerMock<User>();
            var repositoryQuestionMock = new Mock<IQuestionRepository>();
            var repositoryUserMock = new Mock<IUserRepository>();
            var repositoryAnswerMock = new Mock<IAnswerRepository>();
            var repositoryGameMock = new Mock<IGameRepository>();
            var repositoryHub = new HubRepository(userManagerMock.Object, Context, repositoryQuestionMock.Object, repositoryUserMock.Object, repositoryAnswerMock.Object, repositoryGameMock.Object);
            string connectionId = CommonTest.ConnectionIdTest;
            var check = repositoryHub.UpdateConnectionIdUserAsync(connectionId, CommonTest.UserNameTest1,CommonTest.CodeGameTest1);
            var user = Context.Set<User>().Where(e => e.ConnectionId == connectionId).SingleOrDefaultAsync();
            Assert.NotNull(user);
        }

        [Fact]
        public async Task Check_Delete_UserAsync()
        {
            IList<string> ids = new List<string>
            {
                CommonApp.PlayerRole
            };
            var userManagerMock = GetUserManagerMock<User>();
            userManagerMock.Setup(u => u.GetRolesAsync(It.IsAny<User>())).Returns(Task.FromResult(ids));
            userManagerMock.Setup(u => u.DeleteAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            var repositoryQuestionMock = new Mock<IQuestionRepository>();
            var repositoryUserMock = new Mock<IUserRepository>();
            var repositoryAnswerMock = new Mock<IAnswerRepository>();
            var repositoryGameMock = new Mock<IGameRepository>();
            var repositoryHub = new HubRepository(userManagerMock.Object, Context, repositoryQuestionMock.Object, repositoryUserMock.Object, repositoryAnswerMock.Object, repositoryGameMock.Object);
            string connectionId = CommonTest.UserConnectionIdTest1;
            var check = await repositoryHub.DeleteUserAsync(connectionId);
            Assert.NotNull(check);
        }

        [Fact]
        public async Task Check_Start_GameAsync()
        {
            IList<string> ids = new List<string>
            {
                CommonApp.HostRole
            };
            var userManagerMock = GetUserManagerMock<User>();
            userManagerMock.Setup(u => u.GetRolesAsync(It.IsAny<User>())).Returns(Task.FromResult(ids));
            var repositoryQuestionMock = new Mock<IQuestionRepository>();
            var repositoryUserMock = new Mock<IUserRepository>();
            var repositoryAnswerMock = new Mock<IAnswerRepository>();
            var repositoryGameMock = new Mock<IGameRepository>();
            var repositoryHub = new HubRepository(userManagerMock.Object, Context, repositoryQuestionMock.Object, repositoryUserMock.Object, repositoryAnswerMock.Object, repositoryGameMock.Object);
            var question = await repositoryHub.NextQuestionAsync(CommonTest.UserConnectionIdTest1, CommonTest.CodeGameTest1, CommonTest.NumberQuestion);
            Assert.NotNull(question);
        }

        [Fact]
        public async Task Check_Not_Start_Game_With_Role_PlayerAsync()
        {
            IList<string> ids = new List<string>
            {
                CommonApp.PlayerRole
            };
            var userManagerMock = GetUserManagerMock<User>();
            userManagerMock.Setup(u => u.GetRolesAsync(It.IsAny<User>())).Returns(Task.FromResult(ids));

            var repositoryQuestionMock = new Mock<IQuestionRepository>();
            var repositoryUserMock = new Mock<IUserRepository>();
            var repositoryAnswerMock = new Mock<IAnswerRepository>();
            var repositoryGameMock = new Mock<IGameRepository>();
            var repositoryHub = new HubRepository(userManagerMock.Object, Context, repositoryQuestionMock.Object, repositoryUserMock.Object, repositoryAnswerMock.Object, repositoryGameMock.Object);
            var question = await repositoryHub.NextQuestionAsync(CommonTest.UserConnectionIdTest1, CommonTest.CodeGameTest1,CommonTest.NumberQuestion);
            Assert.Null(question);
        }

        [Fact]
        public async Task Check_Not_Submit_Answer_With_Question_Not_ExitsAsync()
        {
            var userManagerMock = GetUserManagerMock<User>();
            var repositoryQuestionMock = new Mock<IQuestionRepository>();
            repositoryQuestionMock.Setup(u => u.CheckQuestionExistAsync(It.IsAny<string>())).Returns(Task.FromResult(false));

            User user = new()
            {
                Id = CommonTest.UserIdTest1,
                Name = CommonTest.UserNameTest1,
                ConnectionId = CommonTest.UserConnectionIdTest1,
            };
            var repositoryUserMock = new Mock<IUserRepository>();
            repositoryUserMock.Setup(u => u.GetUserByConnectionIdAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            var repositoryAnswerMock = new Mock<IAnswerRepository>();
            var repositoryGameMock = new Mock<IGameRepository>();
            var repositoryHub = new HubRepository(userManagerMock.Object, Context, repositoryQuestionMock.Object, repositoryUserMock.Object, repositoryAnswerMock.Object, repositoryGameMock.Object);
            var answer = await repositoryHub.SubmitAnswerAsync(CommonTest.UserConnectionIdTest1, CommonTest.QuestionIdTest, CommonTest.AnswerContentTest, CommonTest.TimeSubmit);
            Assert.Null(answer);
        }

        [Fact]
        public async Task Check_Not_Submit_Answer_With_Not_UserAsync()
        {
            var userManagerMock = GetUserManagerMock<User>();
            var repositoryQuestionMock = new Mock<IQuestionRepository>();
            repositoryQuestionMock.Setup(u => u.CheckQuestionExistAsync(It.IsAny<string>())).Returns(Task.FromResult(true));

            var repositoryUserMock = new Mock<IUserRepository>();
            repositoryUserMock.Setup(u => u.GetUserByConnectionIdAsync(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            var repositoryAnswerMock = new Mock<IAnswerRepository>();
            var repositoryGameMock = new Mock<IGameRepository>();
            var repositoryHub = new HubRepository(userManagerMock.Object, Context, repositoryQuestionMock.Object, repositoryUserMock.Object, repositoryAnswerMock.Object, repositoryGameMock.Object);
            var answer = await repositoryHub.SubmitAnswerAsync(CommonTest.UserConnectionIdTest1, CommonTest.QuestionIdTest, CommonTest.AnswerContentTest, CommonTest.TimeSubmit);
            Assert.Null(answer);
        }

        [Fact]
        public async Task Check_Submit_AnswerAsync()
        {
            var userManagerMock = GetUserManagerMock<User>();

            Question question = new Question
            {
                Id = CommonTest.QuestionIdTest,
                Weight = CommonTest.QuestionWeight,
                Content = CommonTest.QuestionContentTest,
                GameId = CommonTest.UserGameIdTest1,
            };
            var repositoryQuestionMock = new Mock<IQuestionRepository>();
            repositoryQuestionMock.Setup(u => u.GetQuestionByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(question));

            User user = new()
            {
                Id = CommonTest.UserIdTest1,
                Name = CommonTest.UserNameTest1,
                ConnectionId = CommonTest.UserConnectionIdTest1,
            };
            var repositoryUserMock = new Mock<IUserRepository>();
            repositoryUserMock.Setup(u => u.GetUserByConnectionIdAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            var repositoryAnswerMock = new Mock<IAnswerRepository>();
            var repositoryGameMock = new Mock<IGameRepository>();
            Game game = new()
            {
                Id = CommonTest.CodeGameTest1,
                Name = CommonTest.NameGameTest1,
                CodeRoom = CommonTest.CodeGameTest1
            };
            repositoryGameMock.Setup(u => u.FindGameByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(game));

            var repositoryHub = new HubRepository(userManagerMock.Object, Context, repositoryQuestionMock.Object, repositoryUserMock.Object, repositoryAnswerMock.Object, repositoryGameMock.Object);
            var answer = await repositoryHub.SubmitAnswerAsync(CommonTest.UserConnectionIdTest1, CommonTest.QuestionIdTest, CommonTest.AnswerContentTest, CommonTest.TimeSubmit);
            Assert.NotNull(answer);
        }

        [Fact]
        public async Task Can_Mark_Answer_Async()
        {
            List<MarkAnswerDto> listMarkAnswer = new List<MarkAnswerDto>();
            MarkAnswerDto markAnswer = new MarkAnswerDto
            {
                Id = CommonTest.AnswerIdTest,
                IsCorrect = true,
            };
            listMarkAnswer.Add(markAnswer);

            var repositoryUserMock = new Mock<IUserRepository>();
            var user = new User
            {
                Id = CommonTest.UserIdTest1,
                Name = CommonTest.UserNameTest1,
                ConnectionId = CommonTest.UserConnectionIdTest1,
            };
            repositoryUserMock.Setup(u => u.GetUserByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            var userManagerMock = GetUserManagerMock<User>();
            var repositoryQuestionMock = new Mock<IQuestionRepository>();
            var question = new Question
            {
                Id = CommonTest.QuestionIdTest,
                Weight = CommonTest.QuestionWeight,
                Content = CommonTest.QuestionContentTest,
                GameId = CommonTest.UserGameIdTest1,
            };
            repositoryQuestionMock.Setup(u => u.GetQuestionByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(question));

            var repositoryAnswerMock = new Mock<IAnswerRepository>();
            var answer = new Answer
            {
                Id = CommonTest.AnswerIdTest,
                IsCorrect = false,
                Content = CommonTest.AnswerContentTest,
                QuestionId = CommonTest.QuestionIdTest,
                UserId = CommonTest.UserIdTest1,
            };
            repositoryAnswerMock.Setup(u => u.GetAnswerByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(answer));

            var repositoryGameMock = new Mock<IGameRepository>();
            var game = new Game
            {
                Id = CommonTest.IdGameTest1,
                Name = CommonTest.NameGameTest1,
                Status = GameStatus.Starting,
                CodeRoom = CommonTest.CodeGameTest1,
                Updated = DateTime.UtcNow,
                Created = DateTime.UtcNow,
            };
            repositoryGameMock.Setup(u => u.FindGameByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(game));

            var repositoryHub = new HubRepository(userManagerMock.Object, Context, repositoryQuestionMock.Object, repositoryUserMock.Object, repositoryAnswerMock.Object, repositoryGameMock.Object);
            var result = await repositoryHub.MarkAnswersListAsync(listMarkAnswer);
            Assert.Equal(CommonTest.CodeGameTest1, result);
        }

        [Fact]
        public async Task Can_End_Game_Async()
        {
            IList<string> ids = new List<string>
            {
                CommonApp.HostRole
            };

            var repositoryUserMock = new Mock<IUserRepository>();    
            var userManagerMock = GetUserManagerMock<User>();
            userManagerMock.Setup(u => u.GetRolesAsync(It.IsAny<User>())).Returns(Task.FromResult(ids));

            var repositoryQuestionMock = new Mock<IQuestionRepository>();
            var repositoryAnswerMock = new Mock<IAnswerRepository>();          
            var repositoryGameMock = new Mock<IGameRepository>();
            var repositoryHub = new HubRepository(userManagerMock.Object, Context, repositoryQuestionMock.Object, repositoryUserMock.Object, repositoryAnswerMock.Object, repositoryGameMock.Object);
            var result = await repositoryHub.EndGameAsync(CommonTest.UserConnectionIdTest1, CommonTest.CodeGameTest1);
            Assert.True(result);
        }

        [Fact]
        public async Task Can_Mark_An_Answer_Async()
        {
            MarkAnAnswerDto markAnAnswer = new MarkAnAnswerDto
            {
                Id = CommonTest.AnswerIdTest,
                IsCorrect = true,
                PreviousMark = CommonTest.PreviousMark
            };               
            var repositoryUserMock = new Mock<IUserRepository>();
            var user = new User
            {
                Id = CommonTest.UserIdTest1,
                Name = CommonTest.UserNameTest1,
                ConnectionId = CommonTest.UserConnectionIdTest1,
            };
            repositoryUserMock.Setup(u => u.GetUserByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            var userManagerMock = GetUserManagerMock<User>();
            var repositoryQuestionMock = new Mock<IQuestionRepository>();
            var question = new Question
            {
                Id = CommonTest.QuestionIdTest,
                Weight = CommonTest.QuestionWeight,
                Content = CommonTest.QuestionContentTest,
                GameId = CommonTest.UserGameIdTest1,
            };
            repositoryQuestionMock.Setup(u => u.GetQuestionByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(question));

            var repositoryAnswerMock = new Mock<IAnswerRepository>();
            var answer = new Answer
            {
                Id = CommonTest.AnswerIdTest,
                IsCorrect = false,
                Content = CommonTest.AnswerContentTest,
                QuestionId = CommonTest.QuestionIdTest,
                UserId = CommonTest.UserIdTest1,
            };
            repositoryAnswerMock.Setup(u => u.GetAnswerByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(answer));

            var repositoryGameMock = new Mock<IGameRepository>();
            var game = new Game
            {
                Id = CommonTest.IdGameTest1,
                Name = CommonTest.NameGameTest1,
                Status = GameStatus.Starting,
                CodeRoom = CommonTest.CodeGameTest1,
                Updated = DateTime.UtcNow,
                Created = DateTime.UtcNow,
            };
            repositoryGameMock.Setup(u => u.FindGameByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(game));

            var repositoryHub = new HubRepository(userManagerMock.Object, Context, repositoryQuestionMock.Object, repositoryUserMock.Object, repositoryAnswerMock.Object, repositoryGameMock.Object);
            var result = await repositoryHub.MarkAnAnswerAsync(markAnAnswer);
            Assert.Equal(CommonTest.CodeGameTest1, result);
        }

        [Fact]
        public async Task Can_Get_Game_By_Id_Async()
        {
            var repositoryUserMock = new Mock<IUserRepository>();
            var userManagerMock = GetUserManagerMock<User>();
            var repositoryQuestionMock = new Mock<IQuestionRepository>();
            var question = new Question
            {
                Id = CommonTest.QuestionIdTest,
                Weight = CommonTest.QuestionWeight,
                Content = CommonTest.QuestionContentTest,
                GameId = CommonTest.UserGameIdTest1,
            };
            repositoryQuestionMock.Setup(u => u.GetQuestionByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(question));

            var repositoryAnswerMock = new Mock<IAnswerRepository>();
            var answer = new Answer
            {
                Id = CommonTest.AnswerIdTest,
                IsCorrect = false,
                Content = CommonTest.AnswerContentTest,
                QuestionId = CommonTest.QuestionIdTest,
                UserId = CommonTest.UserIdTest1,
            };
            repositoryAnswerMock.Setup(u => u.GetAnswerByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(answer));

            var repositoryGameMock = new Mock<IGameRepository>();
            var game = new Game
            {
                Id = CommonTest.IdGameTest1,
                Name = CommonTest.NameGameTest1,
                Status = GameStatus.Starting,
                CodeRoom = CommonTest.CodeGameTest1,
                Updated = DateTime.UtcNow,
                Created = DateTime.UtcNow,
            };
            repositoryGameMock.Setup(u => u.FindGameByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(game));

            var repositoryHub = new HubRepository(userManagerMock.Object, Context, repositoryQuestionMock.Object, repositoryUserMock.Object, repositoryAnswerMock.Object, repositoryGameMock.Object);
            var result = await repositoryHub.GetGameByIdAnswer(CommonTest.AnswerIdTest);
            Assert.NotNull(result);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
