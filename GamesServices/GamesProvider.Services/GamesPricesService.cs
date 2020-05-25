using AutoMapper;
using DBAccess;
using GamesProvider.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace GamesProvider.Services
{
    public class GamesPricesService : IGamesPricesService
    {
        private readonly GameServiceDBContext _dbContext;
        private readonly IMapper _mapper;

        public GamesPricesService(GameServiceDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<FullGameDTO> GetBestGames(int count)
        {
            var filter = new FilterRequestDTO()
            {
                CountPerPage = count,
                From = 0,
                FilterOptions = new FilterOptionsDTO()
                {
                    AscendingOrder = true,
                    GameName = "",
                    Platforms = _dbContext.Platforms.Select(p => p.PlatformId),
                    SortType = SortType.discount
                }
            };
            return GetByFilter(filter);
        }

        public IEnumerable<FullGameDTO> GetByFilter(FilterRequestDTO filterRequest)
        {
            var filter = filterRequest.FilterOptions;
            if(filter.Platforms.Count() == 0)
            {
                filter.Platforms = _dbContext.Platforms.Select(p => p.PlatformId);
            }
            var gameprices = _dbContext.GamePrices
                .Where(gp => gp.Game.Name.ToLower().Contains(filter.GameName) &&
                       filter.Platforms.Contains(gp.PlatformId));

            if(filter.SortType != SortType.basePrice)
            {
                gameprices = gameprices.Where(gp => gp.BasePrice > gp.DiscountedPrice);
            };

            return  gameprices
                         .Include(gp => gp.Game)
                            .ThenInclude(gp => gp.Images)
                         .Include(gp => gp.Platform)
                         .OrderBy(GetKeySelector(filter.SortType), CreateComparer(filter.AscendingOrder))
                         .Skip(filterRequest.From)
                         .Take(filterRequest.CountPerPage)
                         .GroupBy(gp => gp.Game)
                         .Select(group => GamesPricesGroupMapper.GamePricesToFullGameDTO(group));
        }

        public int GetByFilterCount(FilterRequestDTO filterRequest)
        {
            var filter = filterRequest.FilterOptions;
            var gameprices = _dbContext.GamePrices
                .Where(gp => gp.Game.Name.ToLower().Contains(filter.GameName) &&
                             filter.Platforms.Contains(gp.PlatformId));
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
