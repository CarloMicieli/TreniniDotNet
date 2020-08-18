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
using Xunit.Abstractions;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Wishlists
{
    public class AddItemToWishlistIntegrationTests : AbstractWebApplicationFixture
    {
        public AddItemToWishlistIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var response = await client.PostJsonAsync($"/api/v1/wishlists/{id}/items", new { }, Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldReturn404NotFound_WhenUserIsNotTheWishlistOwner()
        {
            var client = CreateHttpClient("Ciccins", "Pa$$word88");

            var id = CollectingSeedData.Wishlists.NewGeorgeFirstList().Id;

            var newItem = new
            {
                catalogItem = "acme-12456",
                addedDate = DateTime.Now,
                price = new
                {
                    value = 250M,
                    currency = "EUR"
                },
                priority = "High",
                notes = "My notes"
            };

            var response = await client.PostJsonAsync($"/api/v1/wishlists/{id}/items", newItem, Check.Nothing);
            await response.LogAsyncTo(Output);
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
                catalogItem = catalogItem,
                addedDate = DateTime.Now,
                price = new
                {
                    value = 250M,
                    currency = "EUR"
                },
                priority = "High",
                notes = "My notes"
            };

            var response = await client.PostJsonAsync($"/api/v1/wishlists/{id}/items", newItem, Check.Nothing);
            await response.LogAsyncTo(Output);
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
                catalogItem = CatalogSeedData.CatalogItems.NewBemo1254134().Slug.Value,
                addedDate = DateTime.Now,
                price = new
                {
                    value = 250M,
                    currency = "EUR"
                },
                priority = "High",
                notes = "My notes"
            };

            var response = await client.PostJsonAsync($"/api/v1/wishlists/{id}/items", newItem, Check.Nothing);
            await response.LogAsyncTo(Output);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
