using System;
using System.Collections.Generic;
using System.Text;

namespace Parsers.Core
{
    public class QueueSettings
    {
        public string ConnectionString { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
    }
}
