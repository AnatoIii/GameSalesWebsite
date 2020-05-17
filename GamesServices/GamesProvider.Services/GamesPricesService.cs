using DBAccess;
using GamesProvider.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace GamesProvider.Services
{
    public class GamesPricesService : IGamesPricesService
    {
        private readonly GameServiceDBContext _dbContext;

        public GamesPricesService(GameServiceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GamePriceDTO GetGamePriceById(int gamePriceId)
        {
            GamePrices gamePrices = _dbContext.GamePrices
                .Where(gp => gp.GamePriceId == gamePriceId)
                .Include(gp => gp.Game)
                    .ThenInclude(g => g.Images)
                .Include(g => g.Platform).FirstOrDefault();
            return gamePrices == null ? new GamePriceDTO() : GamePricesToDTO(gamePrices);
        }
        public GamePriceDTO GetGamePriceByPlatformAndGame(int platformId, int gameId)
        {
            GamePrices gamePrices = _dbContext.GamePrices
                .Where(gp => gp.PlatformId == platformId & gp.GameId == gameId)
                .Include(gp => gp.Game)
                    .ThenInclude(g => g.Images)
                .Include(g => g.Platform).FirstOrDefault();
            return gamePrices == null ? new GamePriceDTO() : GamePricesToDTO(gamePrices);
        }

        public IEnumerable<GamePriceDTO> GetGamePricesByName(string name, int count, int offset)
        {
            return _dbContext.GamePrices.Where(gp => gp.Game.Name.ToLower().Contains(name.ToLower()))
                .Skip(offset).Take(count)
                .Include(gp => gp.Game)
                    .ThenInclude(g => g.Images)
                .Include(g => g.Platform)
                .Select(gp => GamePricesToDTO(gp));
        }

        public int GetGamePricesByNameCount(string name)
        {
            return _dbContext.GamePrices.Where(gp => gp.Game.Name.ToLower().Contains(name.ToLower())).Count();
        }

        public IEnumerable<GamePriceDTO> GetGamePricesByPlatform(int platformId, int count, int offset)
        {
            return _dbContext.GamePrices.Where(gp => gp.PlatformId == platformId)
                .Skip(offset).Take(count)
                .Include(gp => gp.Game)
                    .ThenInclude(g => g.Images)
                .Include(gp => gp.Platform)
                .Select(gp => GamePricesToDTO(gp));
        }

        public int GetGamePricesByPlatformCount(int platformId)
        {
            return _dbContext.GamePrices.Where(gp => gp.PlatformId == platformId).Count();
        }

        private static GamePriceDTO GamePricesToDTO(GamePrices gamePrices)
        {
            return new GamePriceDTO()
            {
                Platform = gamePrices.Platform,
                Game = gamePrices.Game,
                BasePrice = gamePrices.BasePrice,
                DiscountedPrice = gamePrices.DiscountedPrice,
                PlatformSpecificId = gamePrices.PlatformSpecificId
            };
        }
    }
}
