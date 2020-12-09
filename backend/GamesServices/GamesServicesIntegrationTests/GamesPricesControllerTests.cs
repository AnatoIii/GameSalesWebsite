using FluentAssertions;
using GamesProvider;
using GamesProvider.Services.DTOs;
using GamesServicesTestsInfrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GamesServicesIntegrationTests.Controllers
{
    public class GamesPricesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        public GamesPricesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "GamesPricesController")]
        [Trait("Category", "GetBestGames")]
        public async Task GetBestGames_When_Then_GetByNumber()
        {
            var httpResponse = await _client.GetAsync($"api/gamesPrices/best/3");

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JsonConvert.DeserializeObject<IEnumerable<GameDTO>>(stringResponse);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            gamesResponse.Should().HaveCount(3);
            gamesResponse.First().Platforms.Should().HaveCount(1); // takes only discounted platforms. so platform 3 wouldn`t be included
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "GamesPricesController")]
        [Trait("Category", "GetBestGames")]
        public async Task GetBestGames_When_Then_GetByNumber_MoreThanExist()
        {
            var httpResponse = await _client.GetAsync($"api/gamesPrices/best/100");

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JsonConvert.DeserializeObject<IEnumerable<GameDTO>>(stringResponse);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            gamesResponse.Should().HaveCount(5); // count of discounted games
            gamesResponse.First(g => g.Id == 1).Platforms.Should().HaveCount(1); // check for multiplatform discounts
            gamesResponse.First(g => g.Id == 4).Platforms.Should().HaveCount(2); // check for multiplatform discounts
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "GamesPricesController")]
        [Trait("Category", "GetBestGames")]
        public async Task GetBestGames_When_Then_GetByInvalidNumber()
        {
            var httpResponse = await _client.GetAsync($"api/gamesPrices/best/-1");

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JsonConvert.DeserializeObject<IEnumerable<GameDTO>>(stringResponse);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            gamesResponse.Should().HaveCount(0);
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "GamesPricesController")]
        [Trait("Category", "GetGamesByFilter")]
        public async Task GetGamesByFilter_When_Get2HavingSortedAscendingByBasePrice_Then_SecondRequestHasLessDiscount()
        {
            var httpResponse1 = await _client.GetAsync($"api/gamesPrices?From=0&CountPerPage=2&SortType=basePrice&AscendingOrder=true");
            var stringResponse1 = await httpResponse1.Content.ReadAsStringAsync();
            var gamesResponse1 = JObject.Parse(stringResponse1)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            //here we should get games count
            var gamesCount = 2;
            var httpResponse2 = await _client.GetAsync($"api/gamesPrices?From={gamesCount}&CountPerPage=2&SortType=basePrice&AscendingOrder=true");
            var stringResponse2 = await httpResponse2.Content.ReadAsStringAsync();
            var gamesResponse2 = JObject.Parse(stringResponse2)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            httpResponse1.StatusCode.Should().Be(HttpStatusCode.OK);
            httpResponse2.StatusCode.Should().Be(HttpStatusCode.OK);

            gamesResponse1.Should().HaveCount(1);
            gamesResponse1.First().Platforms.Should().HaveCount(2);
            gamesResponse2.Should().HaveCount(2);

            gamesResponse1.First().BestPrice.Should().BeLessThan(gamesResponse2.First().BestPrice);
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "GamesPricesController")]
        [Trait("Category", "GetGamesByFilter")]
        public async Task GetGamesByFilter_When_GetByCategory_Then_OnlySuchGamesExists()
        {
            var platformsArray = new int[] { 1, 3 };
            var httpResponse = await _client.GetAsync($"api/gamesPrices?From=0&CountPerPage=25&Platforms[0]=1&Platforms[1]=3");
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JObject.Parse(stringResponse)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            gamesResponse.All(game => game.Platforms.Any(platform => platformsArray.Contains(platform.Id))).Should().BeTrue();
        }

        [Theory]
        [InlineData("GAME1")]
        [InlineData("game1")]
        [InlineData("GamE1")]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "GamesPricesController")]
        [Trait("Category", "GetGamesByFilter")]
        public async Task GetGamesByFilter_When_GetByName_Then_OnlySuchGamesExists(string gamePredicate)
        {
            var httpResponse = await _client.GetAsync($"api/gamesPrices?From=0&CountPerPage=25&GameName={gamePredicate}");
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JObject.Parse(stringResponse)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            gamesResponse.Should().NotBeEmpty()
                .And.HaveCount(1);
            gamesResponse.All(game => game.Name.ToLower().Contains(gamePredicate.ToLower())).Should().BeTrue();
        }
    }
}
