using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerBackgroundServices
{
    public class QueueSettings
    {
        public string ConnectionString { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
    }
}
