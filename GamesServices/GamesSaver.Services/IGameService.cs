using GamesSaver.Services.DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesSaver.Services
{
    public interface IGameService
    {
        Game AddGame(GameEntryDTO gameEntry);
    }
}
