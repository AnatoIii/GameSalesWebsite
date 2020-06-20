using Parsers.Core;
using System;
using System.Threading.Tasks;

namespace Parsers.Infrastructure
{
    public class ParserService
    {
        private readonly IParser _rParser;
        private readonly ILogger _rLogger;
        private readonly QueueClient _rClient;

        public ParserService(IParser parser, ILogger logger)
        {
            _rParser = parser;
            _rLogger = logger;
            _rClient = new QueueClient(logger, parser.ParserSettings.QueueSettings);
        }

        public async Task StartService()
        {
            _rLogger.Log($"Process started - {DateTime.Now}");
            var entities = await _rParser.ParsePlatform();
            _rClient.SendEntries(entities);
            _rLogger.Log($"Process ended - {DateTime.Now}");
        }
    }
}
