using FluentAssertions;
using GamesProvider;
using GamesProvider.Services.DTOs;
using GamesServicesTestsInfrastructure;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GamesServicesIntegrationTests
{
    public class SearchTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        public SearchTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "SearchTests")]
        [Trait("Category", "NameFilter")]
        public async Task GetGameByName_When_GameExists_Then_SomeData()
        {
            // target game name from the user
            string gamePredicate = "TestGame1";

            // user click on search, we have a request
            var httpResponse = await _client.GetAsync($"api/gamesPrices?From=0&CountPerPage=25&GameName={gamePredicate}");

            // getting responce
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JObject.Parse(stringResponse)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            // responce has 1 game with target name
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            gamesResponse.Should().NotBeEmpty()
                .And.HaveCount(1);
            gamesResponse.All(game => game.Name.ToLower().Contains(gamePredicate.ToLower())).Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "SearchTests")]
        [Trait("Category", "NameFilter")]
        public async Task GetGameByName_When_GameNoExists_Then_NoData()
        {
            // target game name from the user that is invalid
            string gamePredicate = "NoValid";

            // user click on search, we have a request
            var httpResponse = await _client.GetAsync($"api/gamesPrices?From=0&CountPerPage=25&GameName={gamePredicate}");

            // getting responce
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JObject.Parse(stringResponse)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            // responce is empty
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            gamesResponse.Should().BeEmpty();
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "SearchTests")]
        [Trait("Category", "PlatformFilter")]
        public async Task GetGamesByPlatform_When_PlatformExists_And_GamesExist_Then_SomeData()
        {
            // target platform id from the user
            int platformPredicate = 1;

            // user click on search, we have a request
            var httpResponse = await _client.GetAsync($"api/gamesPrices?From=0&CountPerPage=25&Platforms={platformPredicate}");

            // getting responce
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JObject.Parse(stringResponse)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            // responce has 2 game with target name
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            gamesResponse.Should().NotBeEmpty()
                .And.HaveCount(2); 
            gamesResponse.All(game => game.Platforms.Select(p => p.Id).Contains(platformPredicate)).Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "GamesPricesController")]
        [Trait("Category", "PlatformFilter")]
        public async Task GetGamesByPlatform_When_MoreThanOnePlatform_And_GamesExist_Then_SomeData()
        {
            // target platform id from the user
            var platformsArray = new int[] { 1, 3 };

            // user click on search, we have a request
            var httpResponse = await _client.GetAsync($"api/gamesPrices?From=0&CountPerPage=25&Platforms[0]=1&Platforms[1]=3");

            // getting responce
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JObject.Parse(stringResponse)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            // responce has 2 game with target name
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            gamesResponse.Should().NotBeEmpty()
                .And.HaveCount(4);
            gamesResponse.All(game => game.Platforms.Any(platform => platformsArray.Contains(platform.Id))).Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "GamesPricesController")]
        [Trait("Category", "PlatformFilter")]
        public async Task GetGamesByPlatform_When_PlatformExists__And_GamesNoExist_Then_NoData()
        {
            // target platform id from the user
            int platformPredicate = 4;

            // user click on search, we have a request
            var httpResponse = await _client.GetAsync($"api/gamesPrices?From=0&CountPerPage=25&Platforms={platformPredicate}");

            // getting responce
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JObject.Parse(stringResponse)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            // responce has 2 game with target name
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            gamesResponse.Should().BeEmpty();
        }

        [Theory]
        [MemberData(nameof(PriceFilterData))]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "GamesPricesController")]
        [Trait("Category", "PriceFilter")]
        public async Task GetGamesByPriceFilter_When_GamesExist_Then_SomeData_WithOrdering(string sortType, string ascendingOrder, List<string> expectedGamesOrdering)
        {
            // user click on search, we have a request
            var httpResponse = await _client.GetAsync($"api/gamesPrices?From=0&CountPerPage=20&SortType={sortType}&AscendingOrder={ascendingOrder}");

            // getting responce
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var gamesResponse = JObject.Parse(stringResponse)["games"].Select(j => j.ToObject<GameDTO>()).ToList();

            // responce has 2 game with target name
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            gamesResponse.Should().NotBeEmpty()
                .And.HaveCount(5);

            // check ascending order by test data
            gamesResponse.Select(gR => gR.Name).Should().ContainInOrder(expectedGamesOrdering);
        }

        public static IEnumerable<object[]> PriceFilterData()
        {
            // GetGamesByBasePrice_When_GamesExist_Then_SortedDataByBasePriceInAscendingOrder
            yield return new object[] { "basePrice", "true", new List<string>()
                {
                    "TestGame1",
                    "TestGame2",
                    "TestGame4",
                    "TestGame5",
                    "TestGame3"
                }};

            // GetGamesByBasePriceWithDescendingOrder_When_GamesExist_Then_SortedDataByBasePriceInDescendingOrder
            yield return new object[] { "basePrice", "false", new List<string>()
                {
                    "TestGame3",
                    "TestGame5",
                    "TestGame1",
                    "TestGame4",
                    "TestGame2"
                }};

            // GetGamesByDiscountedPrice_When_GamesExist_Then_SortedDataByDiscountedPriceInAscendingOrder
            yield return new object[] { "discountedPrice", "true", new List<string>()
                {
                    "TestGame1",
                    "TestGame3",
                    "TestGame4",
                    "TestGame5",
                    "TestGame2"
                }};

            // GetGamesByDiscountedPriceWithDescendingOrder_When_GamesExist_Then_SortedDataByDiscountedPriceInDescendingOrder
            yield return new object[] { "discountedPrice", "false", new List<string>()
                {
                    "TestGame2",
                    "TestGame5",
                    "TestGame4",
                    "TestGame3",
                    "TestGame1"
                }};

            // GetGamesByDiscount_When_GamesExist_Then_SortedDataByDiscountInAscendingOrder
            yield return new object[] { "discount", "true", new List<string>()
                {
                    "TestGame2",
                    "TestGame4",
                    "TestGame5",
                    "TestGame1",
                    "TestGame3"
                }};

            // GetGamesByDiscountWithDescendingOrder_When_GamesExist_Then_SortedDataByDiscountInDescendingOrder
            yield return new object[] { "discount", "false", new List<string>()
                {
                    "TestGame3",
                    "TestGame1",
                    "TestGame4",
                    "TestGame5",
                    "TestGame2"
                }};
        }
    }
}
