using DBAccess;
using GamesSaver.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GamesServicesTests
{
    public class GamesTests
    {
        private DbContextOptions DBContextOptionsCreator(string name)
        {
            return new DbContextOptionsBuilder<GameServiceDBContext>()
                            .UseInMemoryDatabase(name)
                            .Options;
        }
        [Fact]
        public void AddGameAddsGameAndIncludedImages()
        {
            GameEntryDTO testInput = new GameEntryDTO()
            {
                BasePrice = 10,
                DiscountedPrice = 1,
                CurrencyId = 1,
                Description = "Test Game Entry",
                Name = "Test",
                PlatformId = 1,
                PlatformSpecificId = "1",
                PictureURLs = new List<string>()
                {
                    "url1",
                    "url2",
                    "url3"
                }
            };

            using (var context = new GameServiceDBContext(DBContextOptionsCreator("AddGameAddsGameAndIncludedImages")))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                GamesSaver.Services.IGameService gameService = new GamesSaver.Services.GameService(context);
                var newGame = gameService.AddGame(testInput);

                Assert.Equal(testInput.Name, newGame.Name);
                Assert.Equal(testInput.PictureURLs, newGame.Images.Select(i => i.URL));
            }
        }
        [Fact]
        public void GetByIdReturnsActualEntryWithImagesOnValidInput()
        {
            Game game = new Game()
            {
                Name = "Test",
                Description = "Test",
                GameId = 1,
                Images = new List<Image>()
                {
                    new Image(){URL = "URL1"}
                }
            };

            using (var context = new GameServiceDBContext(DBContextOptionsCreator("GetByIdReturnsActualEntryWithImagesOnValidInput")))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Games.Add(game);
                context.SaveChanges();
                GamesProvider.Services.IGameService gameService = new GamesProvider.Services.GameService(context);
                var newGame = gameService.GetById(1);

                Assert.NotNull(newGame);
                Assert.NotNull(newGame.Images);
            }
        }
    }
}
