using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Wishlists
{
    public class CreateWishlistIntegrationTests : AbstractWebApplicationFixture
    {
        public CreateWishlistIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task CreateWishlist_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var response = await client.PostJsonAsync("/api/v1/wishlists", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task CreateWishlist_ShouldReturn409Conflict_WhenUserHasAnotherWishlistWithTheSameName()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var request = new
            {
                ListName = "First list",
                Visibility = "Private"
            };

            var response = await client.PostJsonAsync("/api/v1/wishlists", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }


        [Fact]
        public async Task CreateWishlist_ShouldCreateNewWishlist()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var request = new
            {
                ListName = "My second list",
                Visibility = "Private"
            };

            var response = await client.PostJsonAsync("/api/v1/wishlists", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}