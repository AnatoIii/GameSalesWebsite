using AutoMapper;
using GamesProvider.Services.DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesProvider.Services
{
    public class GamePricesMapper : Profile
    {
        public GamePricesMapper()
        {
            CreateMap<GamePrices, GamePriceDTO>();
        }
    }
}
