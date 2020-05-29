using DBAccess;
using GamesSaver.Services.DTOs;
using GamesSaver.Services.Interfaces;
using Models;
using System.Linq;

namespace GamesSaver.Services
{
    public class GameService : IGameService
    {
        private GameServiceDBContext _dbContext;

        public GameService(GameServiceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Game AddGame(GameEntryDTO gameEntry)
        {
            var game = new Game() {
                Name = gameEntry.Name,
                Description = gameEntry.Description,
                Images = gameEntry.PictureURLs
                                 .Select(p => new Image() { URL = p })
                                 .ToList()
            };
            
            var newGame = _dbContext.Games.Add(game).Entity;

            _dbContext.SaveChanges();

            return newGame;
        }
    }
}
