using Parsers.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parsers.Core
{
    public interface IParser
    {
        Task<IEnumerable<GameEntry>> ParsePlatform();
        ParserSettings ParserSettings { get; }
    }
}
