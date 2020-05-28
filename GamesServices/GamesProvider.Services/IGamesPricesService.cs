using GamesProvider.Services.DTOs;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesProvider.Services
{
    public interface IGamesPricesService
    {
        IEnumerable<GameDTO> GetByFilter(FilterRequestDTO filterRequest);
        int GetByFilterCount(FilterRequestDTO filterRequest);
        IEnumerable<GameDTO> GetBestGames(int count);
    }
}
