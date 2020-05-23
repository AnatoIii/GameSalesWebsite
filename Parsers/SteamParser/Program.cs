using Microsoft.Extensions.Configuration;
using Parsers.Core;
using Parsers.Infrastructure;
using System.IO;
using System.Threading.Tasks;

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

            IParser parser = new SteamParser(parserSettings);
            ILogger logger = new ConsoleLogger();

            GetParserResutl(parser, logger).Wait();

            logger.Log($"{nameof(parser)} complete.");
        }

        static async Task GetParserResutl(IParser parser, ILogger logger)
        {
            ParserService parserService = new ParserService(parser, logger);
            var process = parserService.StartService();
            await process;
        }
    }
}
