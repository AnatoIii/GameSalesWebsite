using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesProvider.Services.DTOs
{
    public class GamePriceDTO
    {
        public Game Game { get; set; }
        public Platform Platform { get; set; }
        public int BasePrice { get; set; }
        public int DiscountedPrice { get; set; }
        public string PlatformSpecificId { get; set; }
    }
}
