using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Wishlists
{
    public class RemoveItemFromWishlistIntegrationTests : AbstractWebApplicationFixture
    {
        public RemoveItemFromWishlistIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var response = await client.DeleteJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldReturn404NotFound_WhenUserIsNotTheWishlistOwner()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var wishlist = CollectionSeedData.Wishlists.George_First_List();

            var id = wishlist.Id;
            var itemId = wishlist.Items.First().Id;

            var response = await client.DeleteJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldReturn404NotFound_WhenWishlistItemIsNotFound()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var wishlist = CollectionSeedData.Wishlists.George_First_List();

            var id = wishlist.Id;
            var itemId = Guid.NewGuid();

            var response = await client.DeleteJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldRemoveItemFromWishlist()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var wishlist = CollectionSeedData.Wishlists.George_First_List();

            var id = wishlist.Id;
            var itemId = wishlist.Items.First().Id;

            var response = await client.DeleteJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}