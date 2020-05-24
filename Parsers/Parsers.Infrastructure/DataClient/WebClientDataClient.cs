using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parsers.Infrastructure
{
    /// <summary>
    /// <see cref="IDataClient"/> that works with <see cref="WebClient"/>
    /// </summary>
    public class WebClientDataClient : IDataClient
    {
        protected readonly string _rBaseUrl;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="baseUrl"></param>
        public WebClientDataClient(string baseUrl)
        {
            _rBaseUrl = baseUrl;
        }

        /// <summary>
        /// <see cref="IDataClient.GetContent(int, int)"/>
        /// </summary>
        public Task<string> GetContent(int count, int offset)
        {
            string url = _rBaseUrl.HandleURL(count, offset);

            var result = Task.Run(() => GetContextByURL(url));

            return result;
        }

        /// <summary>
        /// Get page response by <paramref name="targetLink"/>
        /// </summary>
        /// <param name="targetLink">Target URL</param>
        /// <returns>Page context or <see cref="string.Empty"/></returns>
        public string GetContextByURL(string targetLink)
        {
            string result = string.Empty;

            using (var client = new WebClient())
            {
                client.Headers["Content-Type"] = "application/json; charset=UTF-8";
                var response = client.UploadString(targetLink, JsonConvert.SerializeObject(new { License = "", Ubi = "", IrlVilationId = "", IsSecured = "" }));

                result = Regex.Replace(response, @"\t|\n|\r", "");
            }

            return result;
        }
    }
}
