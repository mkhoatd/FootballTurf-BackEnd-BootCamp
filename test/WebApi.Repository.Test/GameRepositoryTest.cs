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
    public class GameRepositoryTest : IDisposable
    {
        #region Seeding
        public GameRepositoryTest()
        {
            ContextOptions = new DbContextOptionsBuilder<AppFootballTurfDbContext>().UseInMemoryDatabase(databaseName: "database_game_2").Options;
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
        public void Can_Create_Room()
        {
            var repositoryGame = new GameRepository(Context);
            Game gameFour = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = CommonTest.NameRoomTest,
                CodeRoom = CommonTest.CodeRoomTest,
                Status = GameStatus.NotStarted,
                Updated = DateTime.UtcNow,
                Created = DateTime.UtcNow,
            };

            var tag = repositoryGame.InsertGameAsync(gameFour).Result;
            var game = Context.Set<Game>().Where(e => e.Id == tag).Single();
            Assert.Equal(tag, game.Id);

        }

        [Fact]
        public void Can_Create_Code_Room()
        {
            var repositoryGame = new GameRepository(Context);
            var tag = repositoryGame.CreateGameCodeAsync().Result;
            var game = Context.Set<Game>().Where(e => e.CodeRoom == tag).ToList().Count;
            int quantityGameByCodeRoom = 0;
            Assert.Equal(quantityGameByCodeRoom, game);

        }

        [Fact]
        public void Can_Find_Room_By_Code()
        {
            var repositoryGame = new GameRepository(Context);
            var tag = repositoryGame.FindGameByCodeRoomAsync(CommonTest.CodeGameTest2).Result;
            var game = Context.Set<Game>().Where(e => e.Id == tag).ToList().Count;
            int quantityGameByGameId = 1;
            Assert.Equal(quantityGameByGameId, game);

        }

        [Fact]
        public void Can_Find_Room_By_Id()
        {
            var LoggerMock = new Mock<ILogger<GameRepository>>();
            var repositoryGame = new GameRepository(Context);
            var tag = repositoryGame.FindGameByIdAsync(CommonTest.IdGameTest1).Result;
            var game = Context.Set<Game>().Where(e => e.Id == tag.Id).ToList().Count;
            int quantityGameByGameId = 1;
            Assert.Equal(quantityGameByGameId, game);

        }

        [Fact]
        public void Check_Start_Game_By_Code_Game()
        {
            var repositoryGame = new GameRepository(Context);
            var check = repositoryGame.CheckStartGameByCodeGameAsync(CommonTest.CodeGameTest1).Result;
            Assert.True(check);
        }

        [Fact]
        public void Check_Not_Start_Game_By_Code_Game()
        {
            var repositoryGame = new GameRepository(Context);
            var check = repositoryGame.CheckStartGameByCodeGameAsync(CommonTest.CodeGameTest3).Result;
            Assert.False(check);
        }

        [Fact]
        public void Check_Create_Cheat_Code_Async()
        {
            var repositoryGame = new GameRepository(Context);
            var check = repositoryGame.CreateCheatCodeAsync(CommonTest.CheatIdGame1, CommonTest.IdGameTest1).Result;
            Assert.NotNull(check);
        }


        [Fact]
        public void Find_Game_Waiting_And_Starting()
        {
            var repositoryGame = new GameRepository(Context);
            var check = repositoryGame.FindGameWaitingAndStartingByUserIdAsync(CommonTest.UserIdTest1).Result;
            Assert.NotNull(check);
        }

        [Fact]
        public void Change_Game_Name()
        {
            var repositoryGame = new GameRepository(Context);
            var check = repositoryGame.EditNameGameAsync(CommonTest.IdGameTest1, CommonTest.NameGameTest2).Result;
            Assert.NotNull(check);
        }

        [Fact]
        public void Delete_Game()
        {
            var repositoryGame = new GameRepository(Context);
            var check = repositoryGame.DeleteGameAsync(CommonTest.IdGameTest1).Result;
            Assert.NotNull(check);
        }

        [Fact]
        public void Update_Status_Game()
        {
            var repositoryGame = new GameRepository(Context);
            var check = repositoryGame.UpdateStatusGameAsync(CommonTest.IdGameTest1, GameStatus.Ended).Result;
            Assert.NotNull(check);
        }

        [Fact]
        public void Update_Save_Game()
        {
            var repositoryGame = new GameRepository(Context);
            var check = repositoryGame.UpdateIsSavedGameAsync(CommonTest.IdGameTest1, true).Result;
            Assert.NotNull(check);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}