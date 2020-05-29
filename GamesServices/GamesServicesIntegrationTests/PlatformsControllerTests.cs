using GamesProvider;
using Microsoft.AspNetCore.Mvc.Testing;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GamesServicesIntegrationTests
{
    public class PlatformsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        public PlatformsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllPlatforms_When_Then_GetAtLeastThreeOfThem()
        {
            var httpResponse = await _client.GetAsync($"api/platforms");

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var platformsResponse = JsonConvert.DeserializeObject<IEnumerable<Platform>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(3 < platformsResponse.Count());
        }
    }
}
