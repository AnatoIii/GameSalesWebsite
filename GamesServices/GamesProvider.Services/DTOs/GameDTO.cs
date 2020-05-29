using System.Collections.Generic;

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
