using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;
using WebApi.Domain.Enum;
using WebApi.Persistence;
using WebApi.Repository.Implementation;
using WebApi.Test.Common;
using Xunit;

namespace WebApi.Repository.Test
{
    public class ReactionsRepositoryTest
    {
        #region Seeding
        public ReactionsRepositoryTest()
        {
            ContextOptions = new DbContextOptionsBuilder<AppFootballTurfDbContext>().UseInMemoryDatabase(databaseName: "database_reactions").Options;
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

            Context.AddRange(one, two, userOne, userTwo, userThree, userFour, question, answer, reactHeart, reactLike, reactSad);
            Context.SaveChanges();

        }

        [Fact]
        public void Get_User_Reaction_By_User_Id_And_Answer_Id()
        {
            var reactionsRepository = new ReactionsRepository(Context);
            var reaction = reactionsRepository.GetUserReactionAsync(CommonTest.UserIdTest2,CommonTest.AnswerIdTest).Result;
            Assert.NotNull(reaction);
        }

        [Fact]
        public void Get_Quantity_Reaction_Heart_By_User_Id()
        {
            var reactionsRepository = new ReactionsRepository(Context);
            var quantity = reactionsRepository.GetQuantityReactHeart(CommonTest.UserIdTest1).Result;
            int quantityHeart = 1;
            Assert.Equal(quantityHeart, quantity);
        }

        [Fact]
        public void Get_Quantity_Reaction_Like_By_User_Id()
        {
            var reactionsRepository = new ReactionsRepository(Context);
            var quantity = reactionsRepository.GetQuantityReactLike(CommonTest.UserIdTest1).Result;
            int quantityHeart = 1;
            Assert.Equal(quantityHeart, quantity);
        }

        [Fact]
        public void Get_Quantity_Reaction_Sad_By_User_Id()
        {
            var reactionsRepository = new ReactionsRepository(Context);
            var quantity = reactionsRepository.GetQuantityReactSad(CommonTest.UserIdTest1).Result;
            int quantityHeart = 1;
            Assert.Equal(quantityHeart, quantity);
        }
    }
}
