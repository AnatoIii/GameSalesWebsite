using Parsers.Core;
using Parsers.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSStoreParser
{
    class PSParser : IParser
    {
        public ParserSettings ParserSettings { get; private set; }
        private readonly RawDataClient _dataClient;
        public PSParser(ParserSettings parserSettings)
        {
            ParserSettings = parserSettings;
            _dataClient = new RawDataClient(ParserSettings.URL);
        }

        public async Task<IEnumerable<GameEntry>> ParsePlatform()
        {
            List<GameEntry> gameEntries = new List<GameEntry>();
            IEnumerable<GameEntry> currentEntries = Enumerable.Empty<GameEntry>();
            int offset = 0;

            do {
                string data = await _dataClient.GetContent(ParserSettings.ElementsPerRequest, offset);
                currentEntries = Deserializer.Deserialize(data, ParserSettings.PlatformId);
                gameEntries.AddRange(currentEntries);
                offset += ParserSettings.ElementsPerRequest;
                await Task.Delay(ParserSettings.PeriodBetweenRequests);
                
            } while (currentEntries.Count() > 0);

            return gameEntries;
        }
    }
}
