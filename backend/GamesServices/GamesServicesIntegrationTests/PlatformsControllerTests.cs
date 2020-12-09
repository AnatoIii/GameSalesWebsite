using FluentAssertions;
using GamesProvider;
using GamesServicesTestsInfrastructure;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GamesServicesIntegrationTests.Controllers
{
    public class PlatformsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public PlatformsControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "PlatformsController")]
        [Trait("Category", "GetAllPlatforms")]
        public async Task GetAllPlatforms_Then_GetAtLeastThreeOfThem()
        {
            var httpResponse = await _client.GetAsync($"api/platforms").ConfigureAwait(false);

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var platformsResponse = JsonConvert.DeserializeObject<IEnumerable<Platform>>(stringResponse);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            platformsResponse.Should().HaveCount(4);
        }
    }
}
