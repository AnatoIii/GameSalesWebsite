using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Parsers.Infrastructure
{
    /// <summary>
    /// <see cref="IDataClient"/> that works with <see cref="WebRequest"/>
    /// </summary>
    public class WebRequestDataClient : IDataClient
    {
        protected readonly string _rBaseUrl;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="baseUrl"></param>
        public WebRequestDataClient(string baseUrl)
        {
            _rBaseUrl = baseUrl;
        }

        /// <summary>
        /// <see cref="IDataClient.GetContent(int, int)"/>
        /// </summary>
        public async Task<string> GetContent(int count, int offset)
        {
            string url = HandleURL(count, offset);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            var response = await request.GetResponseAsync().ConfigureAwait(false);
            HttpWebResponse httpResponse = (HttpWebResponse)response;

            string result = string.Empty;

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                using Stream receiveStream = httpResponse.GetResponseStream();

                using StreamReader readStream = string.IsNullOrWhiteSpace(httpResponse.CharacterSet)
                    ? new StreamReader(receiveStream)
                    : new StreamReader(receiveStream, Encoding.GetEncoding(httpResponse.CharacterSet));

                result = await readStream.ReadToEndAsync();

                httpResponse.Close();
            }

            return result;
        }

        protected virtual string HandleURL(int count, int offset)
            => _rBaseUrl.Replace("@SIZE", count.ToString())
                                 .Replace("@START", offset.ToString());
    }
}
