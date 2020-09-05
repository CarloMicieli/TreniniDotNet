using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Shops
{
    public class GetFavouriteShopsIntegrationTests : AbstractWebApplicationFixture
    {
        public GetFavouriteShopsIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetFavouriteShops_ShouldReturn404_WhenUserHasNoFavouriteShops()
        {
            var client = CreateHttpClient("Ciccins", "Pa$$word88");

            var response = await client.GetAsync($"/api/v1/shops/favourites");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
