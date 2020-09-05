using System;
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
    public class AddItemToCollectionIntegrationTests : AbstractWebApplicationFixture
    {
        public AddItemToCollectionIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task AddItemToCollection_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var response = await client.PostJsonAsync($"api/v1/collections/{id}/items",
                new { }, Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldReturn404NotFound_WhenCollectionWasNotFound()
        {
            var client = CreateHttpClient("Ciccins", "Pa$$word88");

            var id = Guid.NewGuid();
            var request = new
            {
                catalogItem = "acme-60392",
                price = new
                {
                    value = 250M,
                    currency = "EUR"
                },
                condition = "New",
                addedDate = DateTime.Now
            };

            var response = await client.PostJsonAsync($"api/v1/collections/{id}/items",
                request, Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldReturn404NotFound_WhenCatalogItemWasNotFound()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var collection = CollectingSeedData.Collections.NewGeorgeCollection();
            var id = collection.Id;

            var request = new
            {
                catalogItem = "not-found",
                price = new
                {
                    value = 250M,
                    currency = "EUR"
                },
                condition = "New",
                addedDate = DateTime.Now
            };

            var response = await client.PostJsonAsync($"api/v1/collections/{id}/items",
                request, Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldReturn404NotFound_WhenShopWasNotFound()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var collection = CollectingSeedData.Collections.NewGeorgeCollection();
            var id = collection.Id;

            var request = new
            {
                catalogItem = "bemo-1254134",
                price = new
                {
                    value = 250M,
                    currency = "EUR"
                },
                condition = "New",
                shop = "not-found",
                addedDate = DateTime.Now
            };

            var response = await client.PostJsonAsync($"api/v1/collections/{id}/items",
                request, Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldAddItemToCollection()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var collection = CollectingSeedData.Collections.NewGeorgeCollection();
            var id = collection.Id;

            var request = new
            {
                catalogItem = "bemo-1254134",
                price = new
                {
                    value = 250M,
                    currency = "EUR"
                },
                condition = "New",
                addedDate = DateTime.Now
            };

            var response = await client.PostJsonAsync($"api/v1/collections/{id}/items",
                request, Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
