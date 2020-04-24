using FluentAssertions;
using IntegrationTests;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using Xunit;

namespace TreniniDotNet.Web.IntegrationTests.Collection.V1.UseCases.Wishlists
{
    public class EditWishlistItemIntegrationTests : AbstractWebApplicationFixture
    {
        public EditWishlistItemIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task EditWishlistItem_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var response = await client.PutJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task EditWishlistItem_ShouldReturn404NotFound_WhenUserIsNotTheWishlistOwner()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var wishlist = CollectionSeedData.Wishlists.George_First_List();
            var id = wishlist.WishlistId;
            var itemId = wishlist.Items.First().ItemId;

            var response = await client.PutJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EditWishlistItem_ShouldReturn404NotFound_WhenWishlistItemIsNotFound()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var wishlist = CollectionSeedData.Wishlists.George_First_List();

            var id = wishlist.WishlistId;
            var itemId = Guid.NewGuid();

            var response = await client.PutJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EditWishlistItem_ShouldModifyWishlistItem()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var wishlist = CollectionSeedData.Wishlists.George_First_List();
            var id = wishlist.WishlistId;
            var itemId = wishlist.Items.First().ItemId;

            var request = new
            {
                Price = 250M,
                Priority = "High",
                Notes = "My notes"
            };

            var response = await client.PutJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}