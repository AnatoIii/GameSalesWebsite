using Parsers.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parsers.Core
{
    /// <summary>
    /// A general parser interface
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// A Function that encapsulates all actions performed by a Parser.
        /// Returns a final collection of <see cref="GameEntry"/> that are ready to be sent to the DB
        /// </summary>
        Task<IEnumerable<GameEntry>> ParsePlatform();
        ParserSettings ParserSettings { get; }
    }
}
