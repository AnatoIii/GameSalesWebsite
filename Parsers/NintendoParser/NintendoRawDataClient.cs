using HtmlAgilityPack;
using Newtonsoft.Json;
using Parsers.Infrastructure;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NintendoParser
{
    class NintendoRawDataClient : IDataClient
    {
        private HttpClient _client;
        private string _baseUrl;

        public NintendoRawDataClient(string baseUrl)
        {
            _client = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<string> GetContent(int count, int page)
        {
            var request = new Request
            {
                IndexName = "noa_aem_game_en_us",
                Params = $"query=&hitsPerPage={count}&page={page}&facetFilters=%5B%5B%22generalFilters%3ADeals%22%5D%5D"
            };
            var requests = new GetContentDto { Requests = new Request[] { request } };
            var json = JsonConvert.SerializeObject(requests);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_baseUrl, content);

            return await response.Content.ReadAsStringAsync();
        }

        private class GetContentDto
        {
            [JsonProperty("requests")]
            public Request[] Requests { get; set; }
        }

        private class Request
        {
            [JsonProperty("indexName")]
            public string IndexName { get; set; }

            [JsonProperty("params")]
            public string Params { get; set; }
        }
    }

}
