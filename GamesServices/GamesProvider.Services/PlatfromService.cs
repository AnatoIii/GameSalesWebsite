using DBAccess;
using GamesProvider.Services.Interfaces;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace GamesProvider.Services
{
    public class PlatfromService : IPlatformService
    {
        private GameServiceDBContext _dbContext;

        public PlatfromService(GameServiceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            return _dbContext.Platforms.ToList();
        }
    }
}
