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
    public class EditCollectionItemIntegrationTests : AbstractWebApplicationFixture
    {
        public EditCollectionItemIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task EditCollectionItem_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            var response = await client.PutJsonAsync(
                $"api/v1/collections/{id}/items/{itemId}",
                new { }, Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task EditCollectionItem_ShouldReturn404NotFound_WhenCollectionWasNotFound()
        {
            var client = CreateHttpClient("Ciccins", "Pa$$word88");

            var id = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            var request = new
            {
                price = new
                {
                    value = 250M,
                    currency = "EUR"
                },
                condition = "New",
                addedDate = DateTime.Now
            };

            var response = await client.PutJsonAsync(
                $"api/v1/collections/{id}/items/{itemId}",
                request,
                Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EditCollectionItem_ShouldReturn404NotFound_WhenUserIsNotTheCollectionOwner()
        {
            var client = CreateHttpClient("Ciccins", "Pa$$word88");

            var georgeCollection = CollectingSeedData.Collections.NewGeorgeCollection();
            var itemId = Guid.NewGuid();

            var request = new
            {
                price = new
                {
                    value = 250M,
                    currency = "EUR"
                },
                condition = "New",
                addedDate = DateTime.Now
            };

            var response = await client.PutJsonAsync(
                $"api/v1/collections/{georgeCollection.Id}/items/{itemId}",
                request,
                Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EditCollectionItem_ShouldReturn200OK_WhenCollectionItemWasModified()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var georgeCollection = CollectingSeedData.Collections.NewGeorgeCollection();

            var item = georgeCollection.Items.First();

            var request = new
            {
                itemId = item.Id,
                price = new
                {
                    value = 250M,
                    currency = "EUR"
                },
                condition = "New",
                addedDate = DateTime.Now
            };

            var response = await client.PutJsonAsync(
                $"api/v1/collections/{georgeCollection.Id.ToGuid()}/items/{item.Id.ToGuid()}",
                request,
                Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
