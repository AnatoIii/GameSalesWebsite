using Parsers.Core;
using System;
using System.Threading.Tasks;

namespace Parsers.Infrastructure
{
    /// <summary>
    /// Extensions for fluent work with <see cref="IParser"/>
    /// </summary>
    public static class ParserExtensions
    {
        /// <summary>
        /// Gets result of <see cref="IParser.ParsePlatform"/>
        /// </summary>
        /// <param name="this">Target <see cref="IParser"/></param>
        /// <param name="logger">Target <see cref="ILogger"/></param>
        /// <exception cref="ArgumentNullException">If <paramref name="this"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
        public static async Task GetParserResult(this IParser @this, ILogger logger)
        {
            if (@this == null)
                throw new ArgumentNullException($"{nameof(@this)} can`t be null!");

            if (logger == null)
                throw new ArgumentNullException($"{nameof(logger)} can`t be null!");

            ParserService parserService = new ParserService(@this, logger);
            var process = parserService.StartService();
            await process;
        }
    }
}
