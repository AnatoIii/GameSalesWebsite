using GamesProvider.Services.DTOs;
using System.Collections.Generic;

namespace GamesProvider.Services.Interfaces
{
    public interface IGamesPricesService
    {
        IEnumerable<GameDTO> GetByFilter(FilterRequestDTO filterRequest);
        int GetByFilterCount(FilterRequestDTO filterRequest);
        IEnumerable<GameDTO> GetBestGames(int count);
    }
}
