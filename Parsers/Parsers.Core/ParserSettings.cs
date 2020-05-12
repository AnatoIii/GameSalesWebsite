using System;
using System.Collections.Generic;
using System.Text;

namespace Parsers.Core
{
    public class ParserSettings
    {
        public int ElementsPerRequest { get;  set; }
        public string URL { get;  set; }
        public int CurrencyId { get; set; }
        public TimeSpan PeriodBetweenRequests { get;  set; }
        public TimeSpan PeriodBetweenParserActivations { get;  set; }
        public int PlatformId { get;  set; }
        public QueueSettings QueueSettings { get;  set; }
    }
}
