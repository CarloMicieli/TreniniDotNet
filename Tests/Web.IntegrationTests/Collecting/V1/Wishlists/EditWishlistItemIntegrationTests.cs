using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using TreniniDotNet.Web;
using Xunit;
using Xunit.Abstractions;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Wishlists
{
    public class EditWishlistItemIntegrationTests : AbstractWebApplicationFixture
    {
        public EditWishlistItemIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task EditWishlistItem_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var response = await client.PutJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", new { }, Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task EditWishlistItem_ShouldReturn404NotFound_WhenUserIsNotTheWishlistOwner()
        {
            var client = CreateHttpClient("Ciccins", "Pa$$word88");

            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            var id = wishlist.Id;
            var itemId = wishlist.Items.First().Id;

            var response = await client.PutJsonAsync($"/api/v1/wishlists/{id.ToGuid()}/items/{itemId.ToGuid()}", new { }, Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EditWishlistItem_ShouldReturn404NotFound_WhenWishlistItemIsNotFound()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();

            var id = wishlist.Id;
            var itemId = Guid.NewGuid();

            var response = await client.PutJsonAsync($"/api/v1/wishlists/{id.ToGuid()}/items/{itemId}", new { }, Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EditWishlistItem_ShouldModifyWishlistItem()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            var id = wishlist.Id;
            var itemId = wishlist.Items.First().Id;

            var request = new
            {
                Price = 250M,
                Priority = "High",
                Notes = "My notes"
            };

            var response = await client.PutJsonAsync($"/api/v1/wishlists/{id.ToGuid()}/items/{itemId.ToGuid()}", request, Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
