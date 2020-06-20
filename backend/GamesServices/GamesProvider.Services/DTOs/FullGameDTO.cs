using System.Collections.Generic;

namespace GamesProvider.Services.DTOs
{
    public class FullGameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public IEnumerable<string> Images { get; set; }
        public IEnumerable<PlatformGamePrice> Platforms { get; set; }
    }
}
