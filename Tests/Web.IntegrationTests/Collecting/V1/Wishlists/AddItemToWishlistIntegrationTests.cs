using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Wishlists
{
    public class AddItemToWishlistIntegrationTests : AbstractWebApplicationFixture
    {
        public AddItemToWishlistIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var response = await client.PostJsonAsync($"/api/v1/wishlists/{id}/items", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldReturn404NotFound_WhenUserIsNotTheWishlistOwner()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = CollectionSeedData.Wishlists.George_First_List().WishlistId;

            var newItem = new
            {
                CatalogItem = "acme-12456",
                AddedDate = DateTime.Now,
                Price = 250M,
                Priority = "High",
                Notes = "My notes"
            };

            var response = await client.PostJsonAsync($"/api/v1/wishlists/{id}/items", newItem, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldReturn409Conflict_WhenTheCatalogItemIsAlreadyInTheWishlist()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var wishlist = CollectionSeedData.Wishlists.George_First_List();
            var id = wishlist.WishlistId;
            var catalogItem = wishlist.Items.First().CatalogItem.Slug.Value;

            var newItem = new
            {
                CatalogItem = catalogItem,
                AddedDate = DateTime.Now,
                Price = 250M,
                Priority = "High",
                Notes = "My notes"
            };

            var response = await client.PostJsonAsync($"/api/v1/wishlists/{id}/items", newItem, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldAddTheItemToTheWishlist()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var wishlist = CollectionSeedData.Wishlists.George_First_List();
            var id = wishlist.WishlistId;

            var newItem = new
            {
                CatalogItem = CatalogSeedData.CatalogItems.Bemo_1254134().Slug.Value,
                AddedDate = DateTime.Now,
                Price = 250M,
                Priority = "High",
                Notes = "My notes"
            };

            var response = await client.PostJsonAsync($"/api/v1/wishlists/{id}/items", newItem, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}