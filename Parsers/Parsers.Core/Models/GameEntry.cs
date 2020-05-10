using System;
using System.Collections.Generic;
using System.Text;

namespace Parsers.Core.Models
{
    public class GameEntry
    {
        public string PlatformSpecificId { get; set; }
        public int PlatformId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BasePrice { get; set; }
        public int DiscountedPrice { get; set; }
        public IEnumerable<string> PictureURLs { get; set; }
    }
}
