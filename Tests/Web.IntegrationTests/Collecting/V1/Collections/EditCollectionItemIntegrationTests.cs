using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Collections
{
    public class EditCollectionItemIntegrationTests : AbstractWebApplicationFixture
    {
        public EditCollectionItemIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
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

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task EditCollectionItem_ShouldReturn404NotFound_WhenCollectionWasNotFound()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            var request = new
            {
                Price = 250M,
                Condition = "New",
                AddedDate = DateTime.Now
            };

            var response = await client.PutJsonAsync(
                $"api/v1/collections/{id}/items/{itemId}",
                request,
                Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EditCollectionItem_ShouldReturn404NotFound_WhenUserIsNotTheCollectionOwner()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var georgeCollection = CollectingSeedData.Collections.NewGeorgeCollection();
            var itemId = Guid.NewGuid();

            var request = new
            {
                Price = 250M,
                Condition = "New",
                AddedDate = DateTime.Now
            };

            var response = await client.PutJsonAsync(
                $"api/v1/collections/{georgeCollection.Id}/items/{itemId}",
                request,
                Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EditCollectionItem_ShouldReturn200OK_WhenCollectionItemWasModified()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var georgeCollection = CollectingSeedData.Collections.NewGeorgeCollection();

            var item = georgeCollection.Items.First();

            var request = new
            {
                ItemId = item.Id,
                Price = 250M,
                Condition = "New",
                AddedDate = DateTime.Now
            };

            var response = await client.PutJsonAsync(
                $"api/v1/collections/{georgeCollection.Id}/items/{item.Id}",
                request,
                Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
