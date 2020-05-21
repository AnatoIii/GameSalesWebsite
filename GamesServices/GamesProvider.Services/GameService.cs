using DBAccess;
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

        public Game GetById(int id)
        {
            return _dbContext.Games.Where(g => g.GameId == id).Include(g=> g.Images).FirstOrDefault();
        }
    }
}
