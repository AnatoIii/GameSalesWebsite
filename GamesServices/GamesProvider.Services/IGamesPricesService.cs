using GamesProvider.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesProvider.Services
{
    public interface IGamesPricesService
    {
        IEnumerable<GamePriceDTO> GetGamePricesByPlatform(int platformId, int count, int offset);
        int GetGamePricesByPlatformCount(int platformId);
        IEnumerable<GamePriceDTO> GetGamePricesByName(string name, int count, int offset);
        int GetGamePricesByNameCount(string name);
        GamePriceDTO GetGamePriceById(int gamePriceId);
        GamePriceDTO GetGamePriceByPlatformAndGame(int platformId, int gameId);
    }
}
