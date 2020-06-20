using Microsoft.Extensions.Configuration;
using Parsers.Core;
using Parsers.Infrastructure;
using System.IO;

namespace SteamParser
{
    class Program
    {
        /// <summary>
        /// Entry point for Steam parser
        /// </summary>
        static void Main()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();
            ParserSettings parserSettings = configuration.Get<ParserSettings>();

            ILogger logger = new ConsoleLogger();
            IParser parser = new SteamParser(parserSettings, logger);

            parser.GetParserResult(logger).Wait();

            logger.Log($"{nameof(parser)} complete.");
        }
    }
}
