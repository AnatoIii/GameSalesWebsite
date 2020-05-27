using Newtonsoft.Json.Linq;
using Parsers.Core.Models;
using Parsers.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace OriginParser
{
    internal class OriginDeserializer : IDeserializer
    {
        public IEnumerable<GameEntry> Deserialize(string json)
        {
            return null;
        }

        private GameEntry MapEntry(JToken jToken)
        {
            return null;
        }
    }
}
