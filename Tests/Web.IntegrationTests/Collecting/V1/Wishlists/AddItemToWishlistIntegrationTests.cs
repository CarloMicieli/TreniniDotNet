using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
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
            var client = CreateHttpClient("Ciccins", "Pa$$word88");

            var id = CollectingSeedData.Wishlists.NewGeorgeFirstList().Id;

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
            var client = CreateHttpClient("George", "Pa$$word88");

            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            var id = wishlist.Id;
            var catalogItem = wishlist.Items.First().CatalogItem.Slug;

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
            var client = CreateHttpClient("George", "Pa$$word88");

            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            var id = wishlist.Id;

            var newItem = new
            {
                CatalogItem = CatalogSeedData.CatalogItems.NewBemo1254134().Slug.Value,
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
