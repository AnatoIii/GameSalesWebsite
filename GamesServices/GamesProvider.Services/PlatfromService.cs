using DBAccess;
using GamesProvider.Services.DTOs;
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

        public IEnumerable<PlatformDTO> GetPlatforms()
        {
            return _dbContext.Platforms.Select(p => new PlatformDTO()
            {
                Id = p.PlatformId,
                Name = p.PlatformName
            });
        }
    }
}
