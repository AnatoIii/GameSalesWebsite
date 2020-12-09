using GamesSaver.Services.DTOs;
using Models;

namespace GamesSaver.Services.Interfaces
{
    public interface IGameService
    {
        Game AddGame(GameEntryDTO gameEntry);
    }
}
