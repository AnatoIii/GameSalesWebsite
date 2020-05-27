using Parsers.Core;
using Parsers.Core.Models;
using Parsers.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NintendoParser
{
    /// <summary>
    /// <see cref="IParser"/> implementation for Nintendo
    /// </summary>
    public class NintendoParser : IParser
    {
        public ParserSettings ParserSettings { get; private set; }
        private readonly IDataClient _rDataClient;
        private readonly NintendoDeserializer _rDeserializer;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="parserSettings"><see cref="ParserSettings"/></param>
        public NintendoParser(ParserSettings parserSettings)
        {
            ParserSettings = parserSettings;

            _rDataClient = new NintendoRawDataClient(ParserSettings.URL);

            _rDeserializer = new NintendoDeserializer();
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
                        e.PictureURLs = _rDeserializer.GetGameScreenshots(ParserSettings.GameBaseURL, e.PlatformSpecificId);
                        e.PlatformId = ParserSettings.PlatformId;
                        e.CurrencyId = ParserSettings.CurrencyId;
                        return e;
                    });
                gameEntries.AddRange(currentEntries);
                offset += 1;
                await Task.Delay(ParserSettings.PeriodBetweenRequests);
            } while (currentEntries.Count() > 0);
            return gameEntries;
        }
    }
}
