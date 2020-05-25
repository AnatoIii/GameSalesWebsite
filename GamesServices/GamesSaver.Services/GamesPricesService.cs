using DBAccess;
using GamesSaver.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamesSaver.Services
{
    public class GamesPricesService : IGamesPricesService
    {
        private GameServiceDBContext _dbContext;
        private IGameService _gameService;

        public GamesPricesService(GameServiceDBContext dbContext, IGameService gameService)
        {
            _dbContext = dbContext;
            _gameService = gameService;
        }

        public GamesPricesService(GameServiceDBContext dbContext)
        {
            _dbContext = dbContext;
            _gameService = new GameService(dbContext);
        }

        public void SaveGamePrices(IEnumerable<GameEntryDTO> gameEntries)
        {
            var oldPrices = _dbContext.GamePrices.Where(gp => gp.PlatformId == gameEntries.First().PlatformId); //Reset old sales for incoming platform
            foreach(var gp in oldPrices)
            {
                gp.DiscountedPrice = gp.BasePrice;
            }

            var newGameEntries = new List<GameEntryDTO>();
            foreach(var entry in gameEntries)
            {
                GamePrices gamePrices = _dbContext.GamePrices
                    .Where(gp => gp.PlatformId == entry.PlatformId & gp.PlatformSpecificId == entry.PlatformSpecificId).FirstOrDefault(); //Check if entry exists
                if(gamePrices == null)
                {
                    newGameEntries.Add(entry); //Create new entry
                } 
                else
                {
                    gamePrices.BasePrice = entry.BasePrice; //Update existing prices
                    gamePrices.DiscountedPrice = entry.DiscountedPrice;
                }
            };
            AddNewGamePrices(newGameEntries);
            _dbContext.SaveChanges();
        }

        private void AddNewGamePrices(IEnumerable<GameEntryDTO> newGameEntries)
        {
            foreach (var entry in newGameEntries)
            {
                Game game = _dbContext.Games.Where(g => g.Name == entry.Name).FirstOrDefault();
                if (game == null)
                {
                    game = _gameService.AddGame(entry);
                }
                _dbContext.GamePrices.Add(new GamePrices()
                {
                    BasePrice = entry.BasePrice,
                    DiscountedPrice = entry.DiscountedPrice,
                    CurrencyId = entry.CurrencyId,
                    GameId = game.GameId,
                    PlatformId = entry.PlatformId,
                    PlatformSpecificId = entry.PlatformSpecificId,
                    GameLinkPostfix = entry.GameLinkPostfix,
                    Review = entry.Review
                });
            }
        }
    }
}
