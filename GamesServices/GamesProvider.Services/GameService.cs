using DBAccess;
using GamesProvider.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamesProvider.Services
{
    public class GameService : IGameService
    {
        private GameServiceDBContext _dbContext;

        public GameService(GameServiceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public FullGameDTO GetById(int id)
        {
            return _dbContext.GamePrices
                    .Where(gp => gp.GameId == id)
                    .Include(gp => gp.Game)
                        .ThenInclude(g => g.Images)
                    .Include(gp => gp.Platform)
                    .ToList()
                    .GroupBy(gp => gp.Game)
                    .Select(group => GamesPricesGroupMapper.GamePricesToFullGameDTO(group))
                    .FirstOrDefault();
        }
    }
}
