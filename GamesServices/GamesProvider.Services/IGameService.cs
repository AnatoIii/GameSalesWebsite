using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesProvider.Services
{
    public interface IGameService
    {
        Game GetById(int id);
    }
}
