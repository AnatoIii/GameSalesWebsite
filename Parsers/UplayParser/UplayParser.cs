using Parsers.Core;
using Parsers.Core.Models;
using Parsers.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UplayParser
{
    /// <summary>
    /// <see cref="IParser"/> implementation for Uplay
    /// </summary>
    public class UplayParser : IParser
    {
        public ParserSettings ParserSettings { get; private set; }
        private readonly WebClientDataClient _rDataClient;
        private readonly UplayDeserializer _rDeserializer;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="parserSettings"><see cref="ParserSettings"/></param>
        public UplayParser(ParserSettings parserSettings)
        {
            ParserSettings = parserSettings;

            _rDataClient = new WebClientDataClient(ParserSettings.URL);

            _rDeserializer = new UplayDeserializer();
        }

        /// <summary>
        /// <see cref="IParser.ParserSettings"/>
        /// </summary>
        public async Task<IEnumerable<GameEntry>> ParsePlatform()
        {
            List<GameEntry> gameEntries = new List<GameEntry>();
            IEnumerable<GameEntry> currentEntries;
            int offset = 0;

            do
            {
                string data = await _rDataClient.GetContent(ParserSettings.ElementsPerRequest, offset);

                currentEntries = _rDeserializer.Deserialize(data)
                    .Select(e =>
                    {
                        e.Description = _rDeserializer.GetGameDescription(_rDataClient.GetContextByURL(ParserSettings.GameBaseURL + e.PlatformSpecificId));
                        e.PlatformId = ParserSettings.PlatformId;
                        e.CurrencyId = ParserSettings.CurrencyId;
                        return e;
                    });

                gameEntries.AddRange(currentEntries);
                offset += ParserSettings.ElementsPerRequest;
                await Task.Delay(ParserSettings.PeriodBetweenRequests);
            }
            while (currentEntries.Count() > 0);

            return gameEntries;
        }
    }
}
