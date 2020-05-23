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
            string url = _rBaseUrl.HandleURL(count, offset);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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
