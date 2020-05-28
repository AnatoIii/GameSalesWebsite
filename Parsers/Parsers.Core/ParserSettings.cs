using System;

namespace Parsers.Core
{
    public class ParserSettings
    {
        public int ElementsPerRequest { get;  set; }

        public string URL { get;  set; }

        /// <summary>
        /// Base URL for game
        /// Can be usefull for parse data from game page
        /// </summary>
        public string GameBaseURL { get; set; }

        public int CurrencyId { get; set; }

        public TimeSpan PeriodBetweenRequests { get;  set; }

        public TimeSpan PeriodBetweenParserActivations { get;  set; }

        public int PlatformId { get;  set; }

        public QueueSettings QueueSettings { get;  set; }
    }
}
