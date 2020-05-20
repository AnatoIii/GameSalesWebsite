using System;
using System.Collections;
using System.Collections.Generic;

namespace Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
