using Microsoft.Extensions.Configuration;
using Parsers.Core;
using Parsers.Infrastructure;
using System.IO;

namespace PSStoreParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();
            ParserSettings parserSettings = configuration.Get<ParserSettings>();

            IParser parser = new PSParser(parserSettings);
            ILogger logger = new ConsoleLogger();

            parser.GetParserResult(logger).Wait();

            logger.Log($"{nameof(parser)} complete.");
        }
    }
}