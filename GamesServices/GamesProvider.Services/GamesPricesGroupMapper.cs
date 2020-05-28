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
        public static FullGameDTO GamePricesToFullGameDTO(IGrouping<int, GamePrices> grouping)
        {
            var game = grouping.Select(g => g.Game).First();
            return new FullGameDTO()
            {
                Id = game.GameId,
                Descriptions = game.Description,
                Name = game.Name,
                Images = game.Images.Select(i => i.URL),
                Platforms = grouping.Select(gp => new PlatformGamePrice()
                {
                    BasePrice = gp.BasePrice,
                    CurrencyId = gp.CurrencyId,
                    DiscountedPrice = gp.DiscountedPrice,
                    Platform = new PlatformDTO()
                    {
                        Id = gp.Platform.PlatformId,
                        Name = gp.Platform.PlatformName
                    },
                    GameURL = gp.Platform.BaseUrl + gp.PlatformSpecificId
                })
            };
        }
        public static GameDTO GamePricesToGameDTO(IGrouping<int, GamePrices> grouping)
        {
            var minGamePrice = grouping.Where(gp => gp.DiscountedPrice == grouping.Select(g => g.DiscountedPrice).Min()).FirstOrDefault();
            var game = grouping.Select(g => g.Game).First();
            return new GameDTO()
            {
                Id = grouping.Key,
                Description = game.Description,
                Name = game.Name,
                Image = game.Images.Select(i => i.URL).FirstOrDefault(),
                Platforms = grouping.Select(gp => new PlatformDTO()
                {
                    Id = gp.Platform.PlatformId,
                    Name = gp.Platform.PlatformName
                }),
                BestPrice = minGamePrice.DiscountedPrice,
                CurrencyId = minGamePrice.CurrencyId
            };
        }
    }
}
