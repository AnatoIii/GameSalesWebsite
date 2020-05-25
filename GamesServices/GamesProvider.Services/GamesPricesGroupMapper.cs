using GamesProvider.Services.DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamesProvider.Services
{
    public class GamesPricesGroupMapper
    {
        public static FullGameDTO GamePricesToFullGameDTO(IGrouping<Game, GamePrices> g)
        {
            return new FullGameDTO()
            {
                Id = g.Key.GameId,
                Descriptions = g.Key.Description,
                Name = g.Key.Name,
                Images = g.Key.Images.Select(i => i.URL),
                Platforms = g.Select(gp => new PlatformGamePrice()
                {
                    BasePrice = gp.BasePrice,
                    CurrencyId = gp.CurrencyId,
                    DiscountedPrice = gp.DiscountedPrice,
                    Platform = gp.Platform,
                    GameURL = gp.Platform.BaseUrl + gp.GameLinkPostfix
                })
            };
        }
    }
}
