using GamesProvider.Services.DTOs;

namespace GamesProvider.Services.Interfaces
{
    public interface IGameService
    {
        FullGameDTO GetById(int id);
    }
}
