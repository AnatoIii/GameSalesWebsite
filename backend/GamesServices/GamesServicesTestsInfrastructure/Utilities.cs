using DBAccess;
using Models;
using System.Collections.Generic;

namespace GamesServicesTestsInfrastructure
{
    public static class Utilities
    {
        public static void InitializeDbForTests(GameServiceDBContext db)
        {
            db.AddRange(GetTestPlatforms());
            db.AddRange(GetTestGamePrices());
            db.AddRange(GetTestGames());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(GameServiceDBContext db)
        {
            db.Platforms.RemoveRange(db.Platforms);
            InitializeDbForTests(db);
        }

        public static List<Platform> GetTestPlatforms()
        {
            return new List<Platform>()
            {
                new Platform()
                {
                    PlatformId = 1,
                    PlatformName = "TestPlatform1",
                    BaseUrl = "TestUrl1"
                },
                new Platform()
                {
                    PlatformId = 2,
                    PlatformName = "TestPlatform2",
                    BaseUrl = "TestUrl2"
                },
                new Platform()
                {
                    PlatformId = 3,
                    PlatformName = "TestPlatform3",
                    BaseUrl = "TestUrl3"
                },
                new Platform()
                {
                    PlatformId = 4,
                    PlatformName = "TestPlatform4",
                    BaseUrl = "TestUrl4"
                }
            };
        }

        public static List<GamePrices> GetTestGamePrices()
        {
            return new List<GamePrices>()
            {
                new GamePrices()
                {
                    GameId = 1,
                    PlatformId = 1,
                    BasePrice = 120,
                    DiscountedPrice = 50
                },
                new GamePrices()
                {
                    GameId = 1,
                    PlatformId = 2,
                    BasePrice = 100,
                    DiscountedPrice = 100
                },
                new GamePrices()
                {
                    GameId = 1,
                    PlatformId = 3,
                    BasePrice = 100,
                    DiscountedPrice = 100
                },
                new GamePrices()
                {
                    GameId = 2,
                    PlatformId = 2,
                    BasePrice = 110,
                    DiscountedPrice = 105
                },
                new GamePrices()
                {
                    GameId = 3,
                    PlatformId = 3,
                    BasePrice = 150,
                    DiscountedPrice = 55
                },
                new GamePrices()
                {
                    GameId = 4,
                    PlatformId = 1,
                    BasePrice = 115,
                    DiscountedPrice = 75
                },
                new GamePrices()
                {
                    GameId = 4,
                    PlatformId = 3,
                    BasePrice = 120,
                    DiscountedPrice = 70
                },
                new GamePrices()
                {
                    GameId = 5,
                    PlatformId = 3,
                    BasePrice = 140,
                    DiscountedPrice = 90
                }
            };
        }

        public static List<Game> GetTestGames()
        {
            return new List<Game>()
            {
                new Game()
                {
                    GameId = 1,
                    Description = "TestGame1",
                    Name = "TestGame1"
                },
                new Game()
                {
                    GameId = 2,
                    Description = "TestGame2",
                    Name = "TestGame2"
                },
                new Game()
                {
                    GameId = 3,
                    Description = "TestGame3",
                    Name = "TestGame3"
                },
                new Game()
                {
                    GameId = 4,
                    Description = "TestGame4",
                    Name = "TestGame4"
                },
                new Game()
                {
                    GameId = 5,
                    Description = "TestGame5",
                    Name = "TestGame5"
                }
            };
        }
    }
}
