using System.Collections.Generic;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GameSalesApiTests.Helpers
{
    /// <summary>
    /// Helper for <see cref="GameSalesContext"/>
    /// </summary>
    public static class DbHelper
    {
        /// <summary>
        /// Creates test in memory <see cref="GameSalesContext"/> with 
        /// <paramref name="dbName"/> and <paramref name="targetTestData"/>
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <param name="dbName">Target DB name</param>
        /// <param name="targetTestData">Target test data</param>
        public static GameSalesContext GetTestContextWithTargetParams<T>(string dbName, IEnumerable<T> targetTestData)
            where T : class
        {
            var dbContextOptions = new DbContextOptionsBuilder<GameSalesContext>()
                .UseInMemoryDatabase(databaseName: dbName, new InMemoryDatabaseRoot())
                .Options;

            using (var context = new GameSalesContext(dbContextOptions, true))
            {
                context.Set<T>().AddRange(targetTestData);
                context.SaveChanges();
            }

            return new GameSalesContext(dbContextOptions, true);
        }
    }
}
