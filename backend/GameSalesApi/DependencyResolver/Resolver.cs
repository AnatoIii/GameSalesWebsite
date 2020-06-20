using DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyResolver
{
    /// <summary>
    /// Resolver for services and DB
    /// </summary>
    public static class Resolver
    {
        /// <summary>
        /// Add "" to <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services">This</param>
        public static void ConfigureServices(this IServiceCollection services)
        {
            // [DN] at the moment I find it difficult to answer whether we will need this method
            //Business dependencies

            //services.AddTransient<IUsersService, UsersService>();
        }

        /// <summary>
        /// Add DbConnection to <see cref="IServiceCollection"/> with <paramref name="connectionString"/>
        /// </summary>
        /// <param name="services">This</param>
        /// <param name="connectionString">DB connection string</param>
        public static void ConfigureDbConnection(this IServiceCollection services, string connectionString)
        {
            // [DN] TODO: after reso;ving DB configure it type and use connectionString
            services.AddDbContext<GameSalesContext>();
        }
    }
}
