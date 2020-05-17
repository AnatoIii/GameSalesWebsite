using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserFavorite
    {
        public int UserFavoriteId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public bool RecieveNotifications { get; set; }

        public Game Game { get; set; }
        public User User { get; set; }
    }
}
