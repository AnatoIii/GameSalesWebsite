using AutoMapper;
using DBAccess;
using GamesProvider.Services;
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
        private IMapper _mapper;
        public GamesPricesTests()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<GamePrices, GamesProvider.Services.DTOs.GamePriceDTO>();
            });
            _mapper = config.CreateMapper();
        }

        private DbContextOptions DBContextOptionsCreator(string name)
        {
            return new DbContextOptionsBuilder<GameServiceDBContext>()
                            .UseInMemoryDatabase(name)
                            .Options;
        }
        private void FillTestData(GameServiceDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var images = new string[] { "URL1", "URL2", "URL3" }.Select(url => new Image() { URL = url }).ToList();
            var currency = new Currency()
            {
                CurrencyId = 1,
                Name = "TEST"
            };
            var games = new Game[] {
                new Game()
                {
                    GameId = 1,
                    Description = "A test game1",
                    Images = images,
                    Name = "Game1"
                },
                new Game()
                {
                    GameId = 2,
                    Description = "A test game2",
                    Images = images,
                    Name = "Game2"
                },
                new Game()
                {
                    GameId = 3,
                    Description = "A test game3",
                    Images = images,
                    Name = "Game2"
                }
            };
            var platforms = new Platform[]
            {
                new Platform()
                {
                    PlatformId = 1,
                    PlatformName = "TestPlatform"
                },
                new Platform()
                {
                    PlatformId = 2,
                    PlatformName = "TestPlatform2"
                }
            };
            var gamePriceEntries = new List<GamePrices>()
            {
                new GamePrices()
                {
                    Game = games[0],
                    GamePriceId = 1,
                    PlatformSpecificId = "1",
                    Platform = platforms[0]
                },
                new GamePrices()
                {
                    Game = games[1],
                    GamePriceId = 2,
                    PlatformSpecificId = "2",
                    Platform = platforms[0]
                },
                new GamePrices()
                {
                    Game = games[2],
                    GamePriceId = 3,
                    PlatformSpecificId = "3",
                    Platform = platforms[1]
                },
            };
            gamePriceEntries.ForEach(gp =>
            {
                gp.BasePrice = 100;
                gp.DiscountedPrice = 10;
            });
            context.GamePrices.AddRange(gamePriceEntries);
            context.SaveChanges();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ReturnsGamePricesOnValidInput(int gamePriceid)
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("ReturnsGamePricesOnValidInput")))
            {
                FillTestData(context);
                GamesProvider.Services.IGamesPricesService service = new GamesProvider.Services.GamesPricesService(context,_mapper);
                var gamePrice = service.GetGamePriceById(gamePriceid);
                Assert.NotNull(gamePrice);
            }
        }
        [Theory]
        [InlineData(4)]
        public void ReturnsNullOnInValidInput(int gamePriceid)
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("ReturnsNullOnInValidInput")))
            {
                FillTestData(context);
                GamesProvider.Services.IGamesPricesService service = new GamesProvider.Services.GamesPricesService(context, _mapper);
                var gamePrice = service.GetGamePriceById(gamePriceid);
                Assert.Null(gamePrice);
            }
        }
        [Theory]
        [InlineData(1,1)]
        [InlineData(1,2)]
        public void ReturnsGamePricesForValidPlatformAndGameIds(int platformId, int gameId)
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("ReturnsGamePricesForValidPlatformAndGameIds")))
            {
                FillTestData(context);
                GamesProvider.Services.IGamesPricesService service = new GamesProvider.Services.GamesPricesService(context, _mapper);
                var gamePrice = service.GetGamePriceByPlatformAndGame(platformId, gameId);
                Assert.NotNull(gamePrice);
            }
        }
        [Fact]
        public void Returns3ForGamePricesCountWithGamesNamedGame()
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("Returns3ForGamePricesCountWithGamesNamedGame")))
            {
                FillTestData(context);
                GamesProvider.Services.IGamesPricesService service = new GamesProvider.Services.GamesPricesService(context, _mapper);
                int count = service.GetGamePricesByNameCount("Game");
                Assert.Equal(3,count);
            }
        }
        [Fact]
        public void Returns2ForGamePricesCountWithPlatform1()
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("Returns2ForGamePricesCountWithPlatform1")))
            {
                FillTestData(context);
                GamesProvider.Services.IGamesPricesService service = new GamesProvider.Services.GamesPricesService(context, _mapper);
                int count = service.GetGamePricesByPlatformCount(1);
                Assert.Equal(2, count);
            }
        }
        [Fact]
        public void ReturnsActualEntriesForvalidLimitedNameRequset()
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("ReturnsActualEntriesForvalidLimitedRequset")))
            {
                FillTestData(context);
                var expected = context.GamePrices;
                GamesProvider.Services.IGamesPricesService service = new GamesProvider.Services.GamesPricesService(context, _mapper);
                var gamePrices = service.GetGamePricesByName("Game",3,0);
                Assert.Equal(expected.Count(), gamePrices.Count());
            }
        }
        [Fact]
        public void ReturnsActualEntriesForvalidLimitedPlatformRequset()
        {
            using (var context = new GameServiceDBContext(DBContextOptionsCreator("ReturnsActualEntriesForvalidLimitedRequset")))
            {
                FillTestData(context);
                var expected = context.GamePrices.Where(gp => gp.PlatformId == 1);
                GamesProvider.Services.IGamesPricesService service = new GamesProvider.Services.GamesPricesService(context, _mapper);
                var gamePrices = service.GetGamePricesByPlatform(1, 3, 0);
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
                FillTestData(context);
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
