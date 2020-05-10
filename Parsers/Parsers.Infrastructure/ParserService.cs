using Parsers.Core;
using Parsers.Infrastructure;
using Parsers.QueueClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parsers.Infrastructure
{
    public class ParserService
    {
        private readonly IParser _parser;
        private readonly ILogger _logger;
        private readonly Client _client;
        private bool IsAlive = true;
        public ParserService(IParser parser, ILogger logger)
        {
            _parser = parser;
            _logger = logger;
            _client = new Client(logger, parser.ParserSettings.QueueSettings);
        }

        public async Task StartService()
        {
            while (IsAlive)
            {
                _logger.Log($"Process started - {DateTime.Now}");
                var entities = await _parser.ParsePlatform();
                _client.SendEntries(entities);
                _logger.Log($"Process ended - {DateTime.Now}");
                await Task.Delay(_parser.ParserSettings.PeriodBetweenParserActivations);

            }
        }

        public void PauseService()
        {
            IsAlive = false;
        }
        public void ResumeService()
        {
            IsAlive = true;
        }

    }
}
