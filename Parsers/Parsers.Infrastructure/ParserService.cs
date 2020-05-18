﻿using Parsers.Core;
using Parsers.Infrastructure;
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
        private readonly QueueClient _client;
        public ParserService(IParser parser, ILogger logger)
        {
            _parser = parser;
            _logger = logger;
            _client = new QueueClient(logger, parser.ParserSettings.QueueSettings);
        }

        public async Task StartService()
        {
            _logger.Log($"Process started - {DateTime.Now}");
            var entities = await _parser.ParsePlatform();
            _client.SendEntries(entities);
            _logger.Log($"Process ended - {DateTime.Now}");
            await Task.Delay(_parser.ParserSettings.PeriodBetweenParserActivations);
        }
    }
}
