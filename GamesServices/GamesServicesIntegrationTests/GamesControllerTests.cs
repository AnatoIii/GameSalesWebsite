using GamesProvider;
using GamesProvider.Services.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GamesServicesIntegrationTests
{
    public class GamesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        public GamesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetGameById_When_GameExists_Then_SomeData()
        {
            var gameId = Guid.NewGuid(); //Set existing game id here
            var httpResponse = await _client.GetAsync($"api/games/{gameId}");

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JsonConvert.DeserializeObject<FullGameDTO>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.NotNull(gamesResponse);
        }

        [Fact]
        public async Task GetGameById_When_GameNotExists_Then_NoData()
        {
            var httpResponse = await _client.GetAsync($"api/games/-1");

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JsonConvert.DeserializeObject<FullGameDTO>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.Null(gamesResponse);
        }
    }
}
