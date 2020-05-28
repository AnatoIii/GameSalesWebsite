using DBAccess;
using GamesProvider.Services.DTOs;
using GamesProvider.Services.Interfaces;
using GamesProvider.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GamesProvider.Services
{
    public class GamesPricesService : IGamesPricesService
    {
        private readonly GameServiceDBContext _dbContext;

        public GamesPricesService(GameServiceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<GameDTO> GetBestGames(int count)
        {
            var filter = new FilterRequestDTO
            {
                CountPerPage = count,
                From = 0,
                AscendingOrder = true,
                GameName = "",
                Platforms = _dbContext.Platforms.Select(p => p.PlatformId),
                SortType = SortType.discount
            };
            return GetByFilter(filter);
        }

        public IEnumerable<GameDTO> GetByFilter(FilterRequestDTO filter)
        {
            bool platformsAny = filter.Platforms == null || filter.Platforms.Count() > 0;

            var gameprices = _dbContext.GamePrices
                .Where(gp => ((filter.GameName == null) || gp.Game.Name.ToLower().Contains(filter.GameName.ToLower())) &&
                       (platformsAny|| filter.Platforms.Contains(gp.PlatformId)));

            if(filter.SortType != SortType.basePrice)
            {
                gameprices = gameprices.Where(gp => gp.BasePrice > gp.DiscountedPrice);
            };

            return gameprices
                      .Include(gp => gp.Game)
                         .ThenInclude(gp => gp.Images)
                      .Include(gp => gp.Platform)
                      .Skip(filter.From)
                      .Take(filter.CountPerPage)
                      .OrderBy(GetKeySelector(filter.SortType), CreateComparer(filter.AscendingOrder))
                      .ToList()
                      .GroupBy(gp => gp.GameId)
                      .Select(group => GamesPricesGroupMapper.GamePricesToGameDTO(group));
        }

        public int GetByFilterCount(FilterRequestDTO filter)
        {
            var gameprices = _dbContext.GamePrices
                .Where(gp => gp.Game.Name.ToLower().Contains(filter.GameName.ToLower()) &&
                       (filter.Platforms.Count() == 0 || filter.Platforms.Contains(gp.PlatformId)));

            if (filter.SortType != SortType.basePrice)
            {
                gameprices = gameprices.Where(gp => gp.BasePrice > gp.DiscountedPrice);
            };

            return gameprices.Count();
        }

        private IComparer<int> CreateComparer(bool ascendingOrder)
        {
            return ascendingOrder ?
                Comparer<int>.Create((x, y) => x.CompareTo(y) > 0 ? x : y):
                Comparer<int>.Create((x, y) => x.CompareTo(y) > 0 ? y : x);
        }
        

        private Func<GamePrices,int> GetKeySelector(SortType sortType)
        {
            return sortType switch
            {
                SortType.basePrice => (GamePrices gp) => gp.BasePrice,
                SortType.discountedPrice => (GamePrices gp) => gp.DiscountedPrice,
                SortType.discount => (GamePrices gp) => (int)Math.Truncate((double)((gp.BasePrice - gp.DiscountedPrice) / gp.BasePrice) * 100),
                _ => throw new InvalidEnumArgumentException()
            };
        }
    }
}
