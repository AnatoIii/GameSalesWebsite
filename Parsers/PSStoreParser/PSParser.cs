using Parsers.Core;
using Parsers.Core.Models;
using Parsers.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSStoreParser
{
    public class PSParser : IParser
    {
        public ParserSettings ParserSettings { get; private set; }
        private readonly IDataClient _rDataClient;
        private readonly IDeserializer _rDeserializer;

        public PSParser(ParserSettings parserSettings)
        {
            ParserSettings = parserSettings;

            _rDataClient = new RawDataClient(ParserSettings.URL);
            _rDeserializer = new Deserializer();
        }

        public async Task<IEnumerable<GameEntry>> ParsePlatform()
        {
            List<GameEntry> gameEntries = new List<GameEntry>();
            IEnumerable<GameEntry> currentEntries;
            int offset = 0;
            do {
                string data = await _rDataClient.GetContent(ParserSettings.ElementsPerRequest, offset);
                currentEntries = _rDeserializer.Deserialize(data)
                    .Select(e =>
                    {
                        e.PlatformId = ParserSettings.PlatformId;
                        e.CurrencyId = ParserSettings.CurrencyId;
                        return e;
                    });
                gameEntries.AddRange(currentEntries);
                offset += ParserSettings.ElementsPerRequest;
                await Task.Delay(ParserSettings.PeriodBetweenRequests);
            } while (currentEntries.Count() > 0);
            return gameEntries;
        }
    }
}
