using System;
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
    public class DeleteWishlistIntegrationTests : AbstractWebApplicationFixture
    {
        public DeleteWishlistIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task DeleteWishlist_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var response = await client.DeleteJsonAsync($"/api/v1/wishlists/{id}", Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task DeleteWishlist_ShouldReturn404NotFound_WhenUserIsNotTheWishlistOwner()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = CollectionSeedData.Wishlists.George_First_List().Id;

            var response = await client.DeleteJsonAsync($"/api/v1/wishlists/{id}", Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteWishlist_ShouldDeleteWishlist()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var id = CollectionSeedData.Wishlists.George_First_List().Id;

            var response = await client.DeleteJsonAsync($"/api/v1/wishlists/{id}", Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}