using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems
{
    public class DeleteRollingStocksIntegrationTests : AbstractWebApplicationFixture
    {
        public DeleteRollingStocksIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task DeleteRollingStocks_ShouldReturn401Unauthorized_WhenTheUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var itemSlug = "acme-123456";
            var id = Guid.NewGuid();
            var response = await client.DeleteJsonAsync($"/api/v1/catalogItems/{itemSlug}/rollingStocks/{id}", Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task DeleteRollingStocks_ShouldReturn404NotFound_WhenCatalogItemIsNotFound()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var itemSlug = "acme-123456";
            var id = Guid.NewGuid();
            var response = await client.DeleteJsonAsync($"/api/v1/catalogItems/{itemSlug}/rollingStocks/{id}", Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteRollingStocks_ShouldReturn204NoContent_WhenRollingStockIsDeleted()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var item = CatalogSeedData.CatalogItems.NewAcme60392();

            var itemSlug = item.Slug.Value;
            var id = item.RollingStocks.First().Id;

            var response = await client.DeleteJsonAsync($"/api/v1/catalogItems/{itemSlug}/rollingStocks/{id}", Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
