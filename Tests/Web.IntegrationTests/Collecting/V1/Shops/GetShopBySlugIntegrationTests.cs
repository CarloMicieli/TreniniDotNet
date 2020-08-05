using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Collecting.V1.Shops.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Shops
{
    public class GetShopBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        public GetShopBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetShopBySlug_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var slug = "not-found";
            var response = await client.GetAsync($"/api/v1/shops/{slug}");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetShopBySlug_ShouldReturn404_WhenShopIsNotFound()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var slug = "not-found";
            var response = await client.GetAsync($"/api/v1/shops/{slug}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetShopBySlug_ShouldReturnShop()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var slug = "Tecnomodel";
            var response = await client.GetJsonAsync<ShopResponse>($"/api/v1/shops/{slug}");

            response.Slug.Should().Be("tecnomodel");
        }
    }
}