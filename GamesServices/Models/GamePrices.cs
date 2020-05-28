using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class GamePrices
    {
        [Key]
        public int GamePriceId { get; set; }
        public int GameId { get; set; }
        public int PlatformId { get; set; }
        public string PlatformSpecificId { get; set; }
        public int BasePrice { get; set; }
        public int DiscountedPrice { get; set; }
        public int CurrencyId { get; set; }
        public string Review { get; set; }

        public Game Game { get; set; }
        public Currency Currency { get; set; }
        public Platform Platform { get; set; }
    }
}
