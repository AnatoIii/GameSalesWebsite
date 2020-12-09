using GamesProvider.Services.DTOs;
using Models;
using System.Collections.Generic;

namespace GamesProvider.Services.Interfaces
{
    public interface IPlatformService
    {
        IEnumerable<PlatformDTO> GetPlatforms();
    }
}
