using DBAccess;
using GamesSaver.Services.DTOs;
using Models;
using System;
using System.Collections.Generic;
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
            Game newGame = _dbContext.Games.Add(new Game()
            {
                Name = gameEntry.Name,
                Description = gameEntry.Description
            }).Entity;
            _dbContext.SaveChanges();
            var images = gameEntry.PictureURLs
                .Select(p => new Image()
                {
                    GameId = newGame.GameId,
                    URL = p
                });
            _dbContext.Images.AddRange(images);
            _dbContext.SaveChanges();
            return newGame;
        }
    }
}
