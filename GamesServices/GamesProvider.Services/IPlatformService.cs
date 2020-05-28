using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesProvider.Services
{
    public interface IPlatformService
    {
        IEnumerable<Platform> GetPlatforms();
    }
}
