using System;
using System.Collections.Generic;
using System.Text;

namespace GamesSaver.Services.DTOs
{
    public class GameEntryDTO
    /// <summary>
    /// A Game entity that parsers feed to the db service
    /// </summary>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Game review
        /// </summary>
        public string Review { get; set; }
        /// <summary>
        /// A native game id on the specified platform
        /// </summary>
        public string PlatformSpecificId { get; set; }
        public int PlatformId { get; set; }
        /// <summary>
        /// Base game price in coins(cents, kopecks etc.)
        /// </summary>
        public int BasePrice { get; set; }
        /// <summary>
        /// Discounted game price in coins(cents, kopecks etc.)
        /// </summary>
        public int DiscountedPrice { get; set; }
        public int CurrencyId { get; set; }
        public IEnumerable<string> PictureURLs { get; set; }
    }
}
