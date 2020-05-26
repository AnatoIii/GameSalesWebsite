using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess
{
    /// <summary>
    /// Default implementation of <see cref="DbContext"/>
    /// </summary>
    public class GameSalesContext : DbContext
    {
        // Common DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<UserTelegramData> UsersTelegramData { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<Token> Tokens { get; set; }

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions{TContext}"/></param>
        public GameSalesContext(DbContextOptions<GameSalesContext> options) 
            : base(options) 
        { }
    }
}
