using Parsers.Core.Models;
using System.Collections.Generic;

namespace Parsers.Infrastructure
{
    /// <summary>
    /// Interface for deserializer for <see cref="IEnumerable{GameEntry}"/>
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        /// Gets data from target <paramref name="input"/> and returns <see cref="IEnumerable{GameEntry}"/>
        /// </summary>
        /// <param name="input">Target input(json, html)</param>
        /// <returns>
        ///     <see cref="IEnumerable{GameEntry}"/> of all games from <paramref name="input"/>
        /// </returns>
        IEnumerable<GameEntry> Deserialize(string input);
    }
}
