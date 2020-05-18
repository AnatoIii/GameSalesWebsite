using System;
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

        public async Task<string> GetContent(int count, int offset)
        {
            string url = HandleURL(count, offset);

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

        protected virtual string HandleURL(int count, int offset)
            => _rBaseUrl.Replace("@SIZE", count.ToString())
                                 .Replace("@START", offset.ToString());
    }
}
