using Parsers.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PSStoreParser
{
    class RawDataClient
    {
        private HttpClient _client;
        private string _baseUrl;
        public RawDataClient(string baseUrl)
        {
            _client = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<string> GetContent(int count, int offset)
        {
            string url = _baseUrl.Replace("@SIZE",count.ToString())
                                 .Replace("@START",offset.ToString());
            var response = await _client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
