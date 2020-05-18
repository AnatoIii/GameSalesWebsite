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
        public static async Task<string> GetJSONForGameURL(this string @this)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@this);
            var responce = await request.GetResponseAsync().ConfigureAwait(false);
            HttpWebResponse httpResponse = (HttpWebResponse)responce;

            string result = string.Empty;

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = httpResponse.GetResponseStream();
                StreamReader readStream;

                if (string.IsNullOrWhiteSpace(httpResponse.CharacterSet))
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(httpResponse.CharacterSet));

                result = await readStream.ReadToEndAsync();

                httpResponse.Close();
                readStream.Close();
            }

            return result;
        }
    }
}
