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

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Collections
{
    public class RemoveItemFromCollectionIntegrationTests : AbstractWebApplicationFixture
    {
        public RemoveItemFromCollectionIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task RemoveItemFromCollection_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            var response = await client.DeleteJsonAsync(
                $"api/v1/collections/{id}/items/{itemId}",
                Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task RemoveItemFromCollection_ShouldReturn404NotFound_WhenCollectionWasNotFound()
        {
            var client = CreateHttpClient("Ciccins", "Pa$$word88");

            var id = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            var response = await client.DeleteJsonAsync(
                $"api/v1/collections/{id}/items/{itemId}",
                Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task RemoveItemFromCollection_ShouldReturn204NoContent_WhenCollectionItemWasRemoved()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var collection = CollectingSeedData.Collections.NewGeorgeCollection();

            var id = collection.Id;
            var itemId = collection.Items.First().Id;

            var response = await client.DeleteJsonAsync(
                $"api/v1/collections/{id.ToGuid()}/items/{itemId.ToGuid()}",
                Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
