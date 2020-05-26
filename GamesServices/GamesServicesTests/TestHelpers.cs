using DBAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamesServicesTests
{
    public class TestHelpers
    {
        public static void FillTestData(GameServiceDBContext context)
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
    }
}
