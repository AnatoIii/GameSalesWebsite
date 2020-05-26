using AutoMapper;
using DBAccess;
using GamesProvider.Services;
using GamesProvider.Services.DTOs;
using GamesSaver.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Xunit;

namespace GamesServicesTests
{
    public class GamesPricesTests
    {
        public GamesPricesTests()
        {
        }

        private DbContextOptions DBContextOptionsCreator(string name)
        {
            return new DbContextOptionsBuilder<GameServiceDBContext>()
                            .UseInMemoryDatabase(name)
                            .Options;
        }


        private FilterRequestDTO CreateRequestPrototype(string name, IEnumerable<int> platformIds)
        {
            return new FilterRequestDTO()
            {
                CountPerPage = 5,
                From = 0,
                FilterOptions = new FilterOptionsDTO()
                {
                    AscendingOrder = true,
                    GameName = name,
                    Platforms = platformIds,
                    SortType = SortType.basePrice
                }
            };
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(1,2)]
        public void ReturnsGamePricesForValidPlatform(int platformId, int gameId)
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("ReturnsGamePricesForValidPlatformAndGameIds")))
            {
                TestHelpers.FillTestData(context);
                var filter = CreateRequestPrototype("", new int[] { 1 });
                GamesProvider.Services.IGamesPricesService service = new GamesProvider.Services.GamesPricesService(context);
                var gamePrice = service.GetByFilter(filter);
                Assert.NotNull(gamePrice);
            }
        }
        [Fact]
        public void Returns3ForGamePricesCountWithGamesNamedGame()
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("Returns3ForGamePricesCountWithGamesNamedGame")))
            {
                TestHelpers.FillTestData(context);
                var filter = CreateRequestPrototype("Game", new int[] {});
                GamesProvider.Services.IGamesPricesService service = new GamesProvider.Services.GamesPricesService(context);
                int count = service.GetByFilterCount(filter);
                Assert.Equal(3,count);
            }
        }
        [Fact]
        public void Returns2ForGamePricesCountWithPlatform1()
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("Returns2ForGamePricesCountWithPlatform1")))
            {
                TestHelpers.FillTestData(context);
                var filter = CreateRequestPrototype("", new int[] { 1});
                GamesProvider.Services.IGamesPricesService service = new GamesProvider.Services.GamesPricesService(context);
                int count = service.GetByFilterCount(filter);
                Assert.Equal(2, count);
            }
        }
        [Fact]
        public void ReturnsActualEntriesForvalidLimitedNameRequset()
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("ReturnsActualEntriesForvalidLimitedRequset")))
            {
                TestHelpers.FillTestData(context);
                var expected = context.GamePrices;
                var filter = CreateRequestPrototype("Game", new int[] { });
                GamesProvider.Services.IGamesPricesService service = new GamesProvider.Services.GamesPricesService(context);
                var gamePrices = service.GetByFilter(filter);
                Assert.Equal(expected.Count(), gamePrices.Count());
            }
        }
        [Fact]
        public void ReturnsActualEntriesForvalidLimitedPlatformRequset()
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("ReturnsActualEntriesForvalidLimitedRequset")))
            {
                TestHelpers.FillTestData(context);
                var expected = context.GamePrices.Where(gp => gp.PlatformId == 1);
                var filter = CreateRequestPrototype("", new int[] { 1});
                GamesProvider.Services.IGamesPricesService service = new GamesProvider.Services.GamesPricesService(context);
                var gamePrices = service.GetByFilter(filter);
                Assert.Equal(expected.Count(), gamePrices.Count());
            }
        }
        [Fact]
        public void UpdatesGamePriceOnExistingEntry()
        {
            var gameEntry = new GameEntryDTO()
            {
                PlatformId = 1,
                PlatformSpecificId = "1",
                DiscountedPrice = 2
            };
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("UpdatesGamePriceOnExistingEntry")))
            {
                TestHelpers.FillTestData(context);
                GamesSaver.Services.IGamesPricesService service = new GamesSaver.Services.GamesPricesService(context);
                service.SaveGamePrices(new GameEntryDTO[] { gameEntry });
                var updatedGamePrice = context.GamePrices
                    .Where(gp => gp.PlatformId == gameEntry.PlatformId & gp.PlatformSpecificId == gameEntry.PlatformSpecificId).FirstOrDefault();
                Assert.NotNull(updatedGamePrice);
                Assert.Equal(2, updatedGamePrice.DiscountedPrice);
            }
        }
    }
}
