using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using WebApi.Domain.Entities;
using WebApi.Domain.Enum;
using WebApi.Persistence;
using WebApi.Repository.Implementation;
using WebApi.Test.Common;
using Xunit;

namespace WebApi.Repository.Test
{
    public class GameUserRepositoryTest : IDisposable
    {
        #region Seeding
        public GameUserRepositoryTest()
        {
            ContextOptions = new DbContextOptionsBuilder<AppFootballTurfDbContext>().UseInMemoryDatabase(databaseName: "GameUser").Options;
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

            var three = new Game
            {
                Id = CommonTest.IdGameTest3,
                Name = CommonTest.NameGameTest1,
                Status = GameStatus.Waiting,
                CodeRoom = CommonTest.CodeGameTest3,
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
            Context.AddRange(one, two, three, userOne, userTwo, gameUserOne);
            Context.SaveChanges();

        }

        [Fact]
        public void Create_Game_User()
        {
            var repositoryGameUser = new GameUserRepository(Context);
            GameUser gameUserNew = new()
            {
                SourceUserId = CommonTest.UserIdTest1,
                GameId = CommonTest.IdGameTest2,
                Score = 0,
                Status = CommonTest.PlayerActive,
                Updated = DateTime.UtcNow,
                Created = DateTime.UtcNow,
            };

            var check = repositoryGameUser.CreateGameUserAsync(gameUserNew).Result;
            Assert.True(check);
        }

        [Fact]
        public void Get_All_Game_By_UserId()
        {
            var repositoryGameUser = new GameUserRepository(Context);
            var listGameUSer = repositoryGameUser.GetAllGameByUserIdAsync(CommonTest.UserIdTest1).Result;
            Assert.NotNull(listGameUSer);
        }


        public void Dispose()
        {
            Context.Dispose();
        }
    }
}