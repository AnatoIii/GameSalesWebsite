using AutoMapper;
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
        private readonly IMapper _mapper;

        public GamesPricesService(GameServiceDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GamePriceDTO GetGamePriceById(int gamePriceId)
        {
            GamePrices gamePrices = _dbContext.GamePrices
                .Where(gp => gp.GamePriceId == gamePriceId)
                .Include(gp => gp.Game)
                    .ThenInclude(g => g.Images)
                .Include(g => g.Platform).FirstOrDefault();
            return _mapper.Map<GamePrices, GamePriceDTO>(gamePrices);
        }
        public GamePriceDTO GetGamePriceByPlatformAndGame(int platformId, int gameId)
        {
            GamePrices gamePrices = _dbContext.GamePrices
                .Where(gp => gp.PlatformId == platformId & gp.GameId == gameId)
                .Include(gp => gp.Game)
                    .ThenInclude(g => g.Images)
                .Include(g => g.Platform).FirstOrDefault();
            return _mapper.Map<GamePrices, GamePriceDTO>(gamePrices);
        }

        public IEnumerable<GamePriceDTO> GetGamePricesByName(string name, int count, int offset)
        {
            var gamePrices = _dbContext.GamePrices.Where(gp => gp.Game.Name.ToLower().Contains(name.ToLower()))
                .Skip(offset).Take(count)
                .Include(gp => gp.Game)
                    .ThenInclude(g => g.Images)
                .Include(g => g.Platform);
            return _mapper.Map<IEnumerable<GamePrices>, IEnumerable<GamePriceDTO>>(gamePrices);
        }

        public int GetGamePricesByNameCount(string name)
        {
            return _dbContext.GamePrices
                .Where(gp => gp.Game.Name.ToLower().Contains(name.ToLower()))
                .Count();
        }

        public IEnumerable<GamePriceDTO> GetGamePricesByPlatform(int platformId, int count, int offset)
        {
            var gamePrices = _dbContext.GamePrices.Where(gp => gp.PlatformId == platformId)
                .Skip(offset).Take(count)
                .Include(gp => gp.Game)
                    .ThenInclude(g => g.Images)
                .Include(gp => gp.Platform);
            return _mapper.Map<IEnumerable<GamePrices>, IEnumerable<GamePriceDTO>>(gamePrices);
        }

        public int GetGamePricesByPlatformCount(int platformId)
        {
            return _dbContext.GamePrices.Where(gp => gp.PlatformId == platformId).Count();
        }
    }
}
