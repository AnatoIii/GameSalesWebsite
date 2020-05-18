using Parsers.Core;
using Parsers.Core.Models;
using Parsers.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamParser
{
    /// <summary>
    /// <see cref="IParser"/> implementation for Steam
    /// </summary>
    public class SteamParser : IParser
    {
        public ParserSettings ParserSettings { get; private set; }
        private readonly IDataClient _rDataClient;
        private readonly IDeserializer _rDeserializer;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="parserSettings"><see cref="ParserSettings"/></param>
        public SteamParser(ParserSettings parserSettings)
        {
            ParserSettings = parserSettings;

            _rDataClient = new WebRequestDataClient(ParserSettings.URL);

            _rDeserializer = new HtmlDeserializer();
        }

        /// <summary>
        /// <see cref="IParser.ParserSettings"/>
        /// </summary>
        public async Task<IEnumerable<GameEntry>> ParsePlatform()
        {
            HtmlDeserializer deserializer = (HtmlDeserializer)_rDeserializer;

            List<GameEntry> gameEntries = new List<GameEntry>();
            IEnumerable<GameEntry> currentEntries;
            int offset = 0;

            do
            {
                string data = await _rDataClient.GetContent(ParserSettings.ElementsPerRequest, offset);
                
                currentEntries = _rDeserializer.Deserialize(data)
                    .Select(e =>
                    {
                        e.Description = deserializer.GetGameDescription((ParserSettings.GameBaseURL + e.GameLinkPostfix)
                            .GetJSONForGameURL().Result);
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
