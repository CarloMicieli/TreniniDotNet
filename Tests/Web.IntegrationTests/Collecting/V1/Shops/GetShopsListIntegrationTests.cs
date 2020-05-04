using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Collecting.V1.Shops.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Shops
{
    public class GetShopsListIntegrationTests : AbstractWebApplicationFixture
    {
        public GetShopsListIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetShopsList_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync($"/api/v1/shops");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetShopsList_ShouldReturnShopsList()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var response = await client.GetJsonAsync<ShopsListResponse>($"/api/v1/shops");

            response.Results.Should().HaveCount(2);
        }
    }
}