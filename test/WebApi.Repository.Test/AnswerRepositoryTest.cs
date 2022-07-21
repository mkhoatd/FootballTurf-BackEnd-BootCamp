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
    public class AnswerRepositoryTest
    {
        #region Seeding
        public AnswerRepositoryTest()
        {
            ContextOptions = new DbContextOptionsBuilder<AppFootballTurfDbContext>().UseInMemoryDatabase(databaseName: "database_answer").Options;
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

        [Fact]
        public void Check_Get_All_Answer_By_QuestionId()
        {
            var repositoryAnswer = new AnswerRepository(Context);
            var listAnswers = repositoryAnswer.GetAllAnswerByQuestionId(CommonTest.QuestionIdTest, CommonTest.UserIdTest2);
            Assert.Single(listAnswers.Result);
        }

        [Fact]
        public void Check_Get_Answer_By_Id()
        {
            var repositoryAnswer = new AnswerRepository(Context);
            var answer = repositoryAnswer.GetAnswerByIdAsync(CommonTest.QuestionIdTest);
            Assert.NotNull(answer);
        }

        [Fact]
        public async Task Check_All_User_AnswerAsync()
        {
            var repositoryAnswer = new AnswerRepository(Context);
            var answer = await repositoryAnswer.CheckAllUserAnswer(CommonTest.IdGameTest1, CommonTest.UserIdTest2,CommonTest.QuestionIdTest);
            Assert.True(answer);
        }
    }
}
