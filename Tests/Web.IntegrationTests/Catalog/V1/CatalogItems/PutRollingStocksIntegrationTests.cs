using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.Common;
using TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems
{
    public class PutRollingStocksIntegrationTests : AbstractWebApplicationFixture
    {
        public PutRollingStocksIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task PutRollingStocks_ShouldReturn401Unauthorized_WhenTheUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var itemSlug = "acme-123456";
            var id = Guid.NewGuid();
            var response = await client.PutJsonAsync($"/api/v1/catalogItems/{itemSlug}/rollingStocks/{id}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PutRollingStocks_ShouldReturn404NotFound_WhenCatalogItemIsNotFound()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var itemSlug = "acme-123456";
            var id = Guid.NewGuid();
            var response = await client.PutJsonAsync($"/api/v1/catalogItems/{itemSlug}/rollingStocks/{id}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PutRollingStocks_ShouldReturn422UnprocessableEntity_WhenRailwayIsNotFound()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var item = CatalogSeedData.CatalogItems.Acme_60392();

            var itemSlug = item.Slug.Value;
            var id = item.RollingStocks.First().Id.ToGuid();

            var model = new
            {
                railway = "not found"
            };

            var response = await client.PutJsonAsync($"/api/v1/catalogItems/{itemSlug}/rollingStocks/{id}", model, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task PutRollingStocks_ShouldReturn409BadRequest_WhenInputIsInvalid()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var item = CatalogSeedData.CatalogItems.Acme_60392();

            var itemSlug = item.Slug.Value;
            var id = item.RollingStocks.First().Id.ToGuid();

            var model = new
            {
                railway = "fs",
                epoch = "invalid"
            };

            var response = await client.PutJsonAsync($"/api/v1/catalogItems/{itemSlug}/rollingStocks/{id}", model, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PutRollingStocks_ShouldReturn201Created_WhenInputIsInvalid()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var item = CatalogSeedData.CatalogItems.Acme_60392();

            var itemSlug = item.Slug.Value;
            var id = item.RollingStocks.First().Id.ToGuid();

            var model = new
            {
                railway = "fs",
                epoch = "III",
                category = "ElectricLocomotive"
            };

            var response = await client.PutJsonAsync($"/api/v1/catalogItems/{itemSlug}/rollingStocks/{id}", model, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            response.Headers.Should().NotBeEmpty();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/CatalogItems/acme-60392"));

            var content = await response.ExtractContent<PostCatalogItemResponse>();
            content.Slug.Value.Should().Be(Slug.Of("acme-60392"));
        }
    }
}
