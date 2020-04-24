using FluentAssertions;
using IntegrationTests;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests;
using Xunit;

namespace TreniniDotNet.Web.IntegrationTests.Collection.V1.UseCases.Wishlists
{
    public class GetWishlistsByOwnerIntegrationTests : AbstractWebApplicationFixture
    {
        public GetWishlistsByOwnerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetWishlistsByOwner_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var owner = "invalid";
            var response = await client.GetAsync($"/api/v1/wishlists/owner/{owner}");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetWishlistsByOwner_ShouldReturn401Unauthorized_WhenUserIsNotAuthorizedToSeeLists()
        {
            var client = CreateHttpClient();

            var owner = "George";
            var response = await client.GetAsync($"/api/v1/wishlists/owner/{owner}");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetWishlistsByOwner_ShouldReturnWishlists()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var owner = "George";
            var response = await client.GetAsync($"/api/v1/wishlists/owner/{owner}?visibility=private");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}