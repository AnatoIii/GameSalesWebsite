using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesProvider.Services.DTOs
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BestPrice { get; set; }
        public string Image { get; set; }
        public int CurrencyId { get; set; }
        public IEnumerable<PlatformDTO> Platforms { get; set; }
    }
}
