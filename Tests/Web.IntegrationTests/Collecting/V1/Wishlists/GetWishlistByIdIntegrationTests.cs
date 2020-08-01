using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Collecting.V1.Wishlists.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Wishlists
{
    public class GetWishlistByIdIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/wishlists";

        public GetWishlistByIdIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetWishlistById_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var response = await client.GetAsync($"/api/v1/wishlists/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetWishlistById_ShouldReturn404_WhenTheWishlistWasNotFound()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = Guid.NewGuid();
            var response = await client.GetAsync($"/api/v1/wishlists/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetWishlistById_ShouldReturn404_WhenTheUserIsNotTheOwnerOfPrivateWishlist()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = CollectingSeedData.Wishlists.NewGeorgeFirstList().Id;
            var response = await client.GetAsync($"/api/v1/wishlists/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetWishlistById_ShouldReturnWishlist()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var id = CollectingSeedData.Wishlists.NewGeorgeFirstList().Id;
            var wishlist = await client.GetJsonAsync<WishlistResponse>($"/api/v1/wishlists/{id}");

            wishlist.Should().NotBeNull();
            wishlist.Owner.Should().Be("George");
        }
    }
}
