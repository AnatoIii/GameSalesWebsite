using FluentAssertions;
using GamesProvider;
using GamesProvider.Services.DTOs;
using GamesServicesTestsInfrastructure;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GamesServicesIntegrationTests.Controllers
{
    public class GamesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        public GamesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "GamesController")]
        [Trait("Category", "GetGameById")]
        public async Task GetGameById_When_GameExists_Then_SomeData()
        {
            var gameId = 1;
            var httpResponse = await _client.GetAsync($"api/games/{gameId}");

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gameResponse = JsonConvert.DeserializeObject<FullGameDTO>(stringResponse);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            gameResponse.Should().NotBeNull();
            gameResponse.Name.Should().Be("TestGame1");
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "GamesController")]
        [Trait("Category", "GetGameById")]
        public async Task GetGameById_When_GameExists_AndHaveMultiplatform()
        {
            var gameId = 4;
            var httpResponse = await _client.GetAsync($"api/games/{gameId}");

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gameResponse = JsonConvert.DeserializeObject<FullGameDTO>(stringResponse);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            gameResponse.Should().NotBeNull();

            gameResponse.Name.Should().Be("TestGame4");
            gameResponse.Platforms.Should().HaveCount(2)
                .And.SatisfyRespectively(
                first =>
                {
                    first.Platform.Id = 1;
                    first.Platform.Name = "TestPlatform1";
                },
                second =>
                {
                    second.Platform.Id = 3;
                    second.Platform.Name = "TestPlatform3";
                });
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "GamesController")]
        [Trait("Category", "GetGameById")]
        public async Task GetGameById_When_GameNotExists_Then_NoData()
        {
            var httpResponse = await _client.GetAsync($"api/games/-1");

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JsonConvert.DeserializeObject<FullGameDTO>(stringResponse);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            gamesResponse.Should().BeNull();
        }
    }
}
