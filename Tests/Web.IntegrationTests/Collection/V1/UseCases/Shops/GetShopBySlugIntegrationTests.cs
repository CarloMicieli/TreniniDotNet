using FluentAssertions;
using IntegrationTests;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests;
using TreniniDotNet.IntegrationTests.Collection.V1.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using Xunit;

namespace TreniniDotNet.Web.IntegrationTests.Collection.V1.UseCases.Shops
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
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var slug = "not-found";
            var response = await client.GetAsync($"/api/v1/shops/{slug}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetShopBySlug_ShouldReturnShop()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var slug = "Tecnomodel";
            var response = await client.GetJsonAsync<ShopResponse>($"/api/v1/shops/{slug}");

            response.Slug.Should().Be("tecnomodel");
        }
    }
}