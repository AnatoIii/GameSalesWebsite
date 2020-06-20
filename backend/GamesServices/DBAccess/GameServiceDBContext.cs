using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBAccess
{
    public class GameServiceDBContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<GamePrices> GamePrices { get; set; }

        public DbSet<Platform> Platforms { get; set; }

        public DbSet<UserComment> UserComments { get; set; }

        public DbSet<UserFavorite> UserFavorites { get; set; }
        public GameServiceDBContext(DbContextOptionsBuilder<GameServiceDBContext> builder) : base(builder.Options)
        {
        }

        public GameServiceDBContext(DbContextOptions options) : base(options)
        {
            if (Database.GetPendingMigrations().Count() > 0)
            {
                Database.Migrate();
            }
        }
    }
}
