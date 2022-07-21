using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
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
    public class QuestionRepositoryTest
    {
        #region Seeding
        public QuestionRepositoryTest()
        {
            ContextOptions = new DbContextOptionsBuilder<AppFootballTurfDbContext>().UseInMemoryDatabase(databaseName: "database_question").Options;
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

            var question = new Question
            {
                Id = CommonTest.QuestionIdTest,
                Weight = CommonTest.QuestionWeight,
                Content = CommonTest.QuestionContentTest,
                GameId = CommonTest.UserGameIdTest1,
            };

            Context.AddRange(one, two, userOne, userTwo, question);
            Context.SaveChanges();

        }

        [Fact]
        public void Can_Create_Question()
        {
            var LoggerMock = new Mock<ILogger<QuestionRepository>>();
            var repositoryQuestionMock = new QuestionRepository(Context, LoggerMock.Object);
            var questionNew = new Question
            {
                Id = CommonTest.QuestionIdTestTwo,
                Weight = CommonTest.QuestionWeight,
                Content = CommonTest.QuestionContentTestTwo,
                GameId = CommonTest.UserGameIdTest1,
            };

            var questionTwo = repositoryQuestionMock.CreateQuestionAsync(questionNew).Result;
            Assert.NotNull(questionTwo);
        }

        [Fact]
        public void Get_Quantity_Question_By_Game_Id()
        {
            var LoggerMock = new Mock<ILogger<QuestionRepository>>();
            var repositoryQuestionMock = new QuestionRepository(Context, LoggerMock.Object);
            var quantityCheck = repositoryQuestionMock.GetQuantityQuestionByIdGameAsync(CommonTest.UserGameIdTest1).Result;
            var quantityQuestionCurrent = 1;
            Assert.Equal(quantityQuestionCurrent, quantityCheck);
        }

        [Fact]
        public void Get_Question_By_Id()
        {
            var LoggerMock = new Mock<ILogger<QuestionRepository>>();
            var repositoryQuestionMock = new QuestionRepository(Context, LoggerMock.Object);
            var questionCurrent = repositoryQuestionMock.GetQuestionByIdAsync(CommonTest.QuestionIdTest).Result;
            Assert.NotNull(questionCurrent);
        }

        [Fact]
        public void Can_Create_Question_Return_Question()
        {
            var LoggerMock = new Mock<ILogger<QuestionRepository>>();
            var repositoryQuestionMock = new QuestionRepository(Context, LoggerMock.Object);
            var questionNew = new Question
            {
                Id = CommonTest.QuestionIdTestTwo,
                Weight = CommonTest.QuestionWeight,
                Content = CommonTest.QuestionContentTestTwo,
                GameId = CommonTest.UserGameIdTest1,
            };

            var questionTwo = repositoryQuestionMock.CreateQuestionReturnQuestionnAsync(questionNew).Result;
            Assert.NotNull(questionTwo);
        }

        [Fact]
        public void Get_All_Question()
        {
            var LoggerMock = new Mock<ILogger<QuestionRepository>>();
            var repositoryQuestionMock = new QuestionRepository(Context, LoggerMock.Object);

            var listGame = repositoryQuestionMock.GetAllQuestionByGameId(CommonTest.IdGameTest1).Result;
            Assert.NotNull(listGame);
        }

        [Fact]
        public void Can_Delete_Question()
        {
            var LoggerMock = new Mock<ILogger<QuestionRepository>>();
            var repositoryQuestionMock = new QuestionRepository(Context, LoggerMock.Object);

            var questionTwo = repositoryQuestionMock.DeleteQuestionByIdQuestion(CommonTest.QuestionIdTest).Result;
            Assert.NotNull(questionTwo);
        }

        [Fact]
        public void Can_Delete_All_Question()
        {
            var LoggerMock = new Mock<ILogger<QuestionRepository>>();
            var repositoryQuestionMock = new QuestionRepository(Context, LoggerMock.Object);

            var result = repositoryQuestionMock.DeleteAllQuestion(CommonTest.IdGameTest1).Result;
            Assert.True(result);
        }

        [Fact]
        public void Can_Get_Number_Question_By_GameId()
        {
            var LoggerMock = new Mock<ILogger<QuestionRepository>>();
            var repositoryQuestionMock = new QuestionRepository(Context, LoggerMock.Object);

            var result = repositoryQuestionMock.GetNumberQuesitionByGameId(CommonTest.IdGameTest1).Result;
            Assert.Equal(1,result);
        }
    }
}
