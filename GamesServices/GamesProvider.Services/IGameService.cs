using GamesProvider.Services.DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesProvider.Services
{
    public interface IGameService
    {
        FullGameDTO GetById(int id);
    }
}
