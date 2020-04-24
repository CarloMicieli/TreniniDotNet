using FluentAssertions;
using IntegrationTests;
using System;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collection.V1.UseCases.Collections
{
    public class AddItemToCollectionIntegrationTests : AbstractWebApplicationFixture
    {
        public AddItemToCollectionIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task AddItemToCollection_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var response = await client.PostJsonAsync($"api/v1/collections/{id}/items",
                new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldReturn404NotFound_WhenCollectionWasNotFound()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = Guid.NewGuid();
            var request = new
            {
                CatalogItem = "acme-60392",
                Price = 250M,
                Condition = "New",
                AddedDate = DateTime.Now
            };

            var response = await client.PostJsonAsync($"api/v1/collections/{id}/items",
                request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldReturn404NotFound_WhenCatalogItemWasNotFound()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;

            var request = new
            {
                CatalogItem = "not-found",
                Price = 250M,
                Condition = "New",
                AddedDate = DateTime.Now
            };

            var response = await client.PostJsonAsync($"api/v1/collections/{id}/items",
                request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldReturn404NotFound_WhenShopWasNotFound()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;

            var request = new
            {
                CatalogItem = "bemo-1254134",
                Price = 250M,
                Condition = "New",
                Shop = "not-found",
                AddedDate = DateTime.Now
            };

            var response = await client.PostJsonAsync($"api/v1/collections/{id}/items",
                request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldAddItemToCollection()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;

            var request = new
            {
                CatalogItem = "bemo-1254134",
                Price = 250M,
                Condition = "New",
                AddedDate = DateTime.Now
            };

            var response = await client.PostJsonAsync($"api/v1/collections/{id}/items",
                request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}