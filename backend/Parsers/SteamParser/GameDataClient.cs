using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Parsers.Infrastructure
{
    /// <summary>
    /// Data client to work with steam game page
    /// </summary>
    public static class GameDataClient
    {
        /// <summary>
        /// Gets JSON data from game page by target URL
        /// </summary>
        /// <param name="this">Target URL</param>
        public static async Task<string> GetJSONForGameURL(this string @this, ILogger logger)
        {
            string result = string.Empty;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@this);
                var response = await request.GetResponseAsync().ConfigureAwait(false);
                HttpWebResponse httpResponse = (HttpWebResponse)response;

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    using Stream receiveStream = httpResponse.GetResponseStream();
                    using StreamReader readStream = string.IsNullOrWhiteSpace(httpResponse.CharacterSet)
                        ? new StreamReader(receiveStream)
                        : new StreamReader(receiveStream, Encoding.GetEncoding(httpResponse.CharacterSet));

                    result = await readStream.ReadToEndAsync();

                    httpResponse.Close();
                }
            }
            catch
            {
                logger.Log($"Error while processing request to {@this}.");
            }

            return result;
        }
    }
}
