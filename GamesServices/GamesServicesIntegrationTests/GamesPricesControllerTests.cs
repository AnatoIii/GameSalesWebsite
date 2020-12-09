using GamesProvider;
using GamesProvider.Services.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GamesServicesIntegrationTests
{
	public class GamesPricesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        public GamesPricesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetBestGames_When_Then_GetByNumber()
        {
            var httpResponse = await _client.GetAsync($"api/gamesPrices/best/9");

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JsonConvert.DeserializeObject<IEnumerable<GameDTO>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(9 == gamesResponse.Count());
        }

        [Fact]
        public async Task GetGamesByFilter_When_Get4HavingSortedAscendingByBasePrice_Then_SecondRequestHasLessDiscount()
        {
            var httpResponse1 = await _client.GetAsync($"api/gamesPrices?From=0&CountPerPage=4&SortType=basePrice&AscendingOrder=true");
            var stringResponse1 = await httpResponse1.Content.ReadAsStringAsync();
            var gamesResponse1 = JObject.Parse(stringResponse1)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            //here we should get games count
            var gamesCount = 250; //gamesResponse.Count - 5;
            var httpResponse2 = await _client.GetAsync($"api/gamesPrices?From={gamesCount}&CountPerPage=4&SortType=basePrice&AscendingOrder=true");
            var stringResponse2 = await httpResponse2.Content.ReadAsStringAsync();
            var gamesResponse2 = JObject.Parse(stringResponse2)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            Assert.Equal(HttpStatusCode.OK, httpResponse1.StatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse2.StatusCode);
            Assert.True(4 == gamesResponse1.Count());
            Assert.True(4 == gamesResponse2.Count());
            Assert.True(gamesResponse1.First().BestPrice < gamesResponse2.First().BestPrice);
        }

        [Fact]
        public async Task GetGamesByFilter_When_GetByCategory_Then_OnlySuchGamesExists()
        {
            var platformsArray = new int[] { 1, 3 };
            var httpResponse = await _client.GetAsync($"api/gamesPrices?From=0&CountPerPage=25&Platforms[0]=1&Platforms[1]=3");
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JObject.Parse(stringResponse)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(gamesResponse.All(game => game.Platforms.Any(platform => platformsArray.Contains(platform.Id))));
        }
    }
}
