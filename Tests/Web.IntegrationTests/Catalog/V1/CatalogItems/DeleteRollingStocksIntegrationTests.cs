using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
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
    }
}
