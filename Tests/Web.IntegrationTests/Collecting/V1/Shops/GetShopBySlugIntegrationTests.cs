using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Collecting.V1.Shops.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;
using Xunit.Abstractions;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Shops
{
    public class GetShopBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        public GetShopBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task GetShopBySlug_ShouldReturn404_WhenShopIsNotFound()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var slug = "not-found";
            var response = await client.GetAsync($"/api/v1/shops/{slug}");

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetShopBySlug_ShouldReturnShop()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var slug = "tecnomodel";
            var response = await client.GetAsync($"/api/v1/shops/{slug}");

            await response.LogAsyncTo(Output);
            var shopResponse = await response.ExtractContent<ShopResponse>();

            shopResponse.Name.Should().Be("Tecnomodel");
        }
    }
}
