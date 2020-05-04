using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Shops
{
    public class CreateShopIntegrationTests : AbstractWebApplicationFixture
    {
        public CreateShopIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task CreateShop_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var response = await client.PostJsonAsync("/api/v1/shops", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }


        [Fact]
        public async Task CreateShop_ShouldCreateNewShops()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var request = new
            {
                Name = "My shop",
                WebsiteUrl = "https://www.shop.com"
            };

            var response = await client.PostJsonAsync("/api/v1/shops", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}