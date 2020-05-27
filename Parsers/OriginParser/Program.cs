using Microsoft.Extensions.Configuration;
using Parsers.Core;
using Parsers.Infrastructure;
using System;
using System.IO;

namespace OriginParser
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

            IParser parser = new OriginParser(parserSettings);
            AsyncMain(parser);
            Console.ReadKey();

            //ILogger logger = new ConsoleLogger();

            //parser.GetParserResult(logger).Wait();

            //logger.Log($"{nameof(parser)} complete.");
        }

        private static async void AsyncMain(IParser parser)
        {
            await parser.ParsePlatform();
        }
    }
}
