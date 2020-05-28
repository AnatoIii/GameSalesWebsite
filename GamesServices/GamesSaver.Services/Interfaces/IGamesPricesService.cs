using GamesSaver.Services.DTOs;
using System.Collections.Generic;

namespace GamesSaver.Services.Interfaces
{
    public interface IGamesPricesService
    {
        void SaveGamePrices(IEnumerable<GameEntryDTO> gameEntries);
    }
}
