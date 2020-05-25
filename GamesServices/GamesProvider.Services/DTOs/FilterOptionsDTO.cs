using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesProvider.Services.DTOs
{
    public class FilterOptionsDTO
    {
        public string GameName { get; set; } = "";
        public IEnumerable<int> Platforms { get; set; }
        public SortType SortType { get; set; } = SortType.basePrice;
        public bool AscendingOrder { get; set; } = true;
    }
}
