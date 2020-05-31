using DBAccess;
using GamesSaver.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GamesServicesTests
{
    public class GamesTests
    {
        private DbContextOptionsBuilder<GameServiceDBContext> DBContextOptionsCreator(string name)
        {
            return new DbContextOptionsBuilder<GameServiceDBContext>()
                            .UseInMemoryDatabase(name);
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
                GamesSaver.Services.Interfaces.IGameService gameService = new GamesSaver.Services.GameService(context);
                var newGame = gameService.AddGame(testInput);

                Assert.Equal(testInput.Name, newGame.Name);
                Assert.Equal(testInput.PictureURLs, newGame.Images.Select(i => i.URL));
            }
        }
        [Fact]
        public void GetByIdReturnsActualEntryWithImagesOnValidInput()
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("GetByIdReturnsActualEntryWithImagesOnValidInput")))
            {
                TestHelpers.FillTestData(context);
                GamesProvider.Services.Interfaces.IGameService gameService = new GamesProvider.Services.GameService(context);
                var newGame = gameService.GetById(1);

                Assert.NotNull(newGame);
                Assert.NotNull(newGame.Images);
            }
        }
    }
}
