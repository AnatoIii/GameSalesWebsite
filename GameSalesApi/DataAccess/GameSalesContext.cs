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
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions{TContext}"/></param>
        public GameSalesContext(DbContextOptions<GameSalesContext> options) : base(options) { }
    }
}
