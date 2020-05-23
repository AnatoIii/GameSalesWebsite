using System.Net.Http;
using System.Threading.Tasks;

namespace Parsers.Infrastructure
{
    /// <summary>
    /// Interface for data clients
    /// </summary>
    public interface  IDataClient
    {
        /// <summary>
        /// Get content by target url with target element per page <paramref name="count"/> 
        /// and with <paramref name="offset"/>
        /// </summary>
        /// <param name="count">Target elements per page count</param>
        /// <param name="offset">Target elements offset</param>
        /// <returns></returns>
        Task<string> GetContent(int count, int offset);
    }
}
