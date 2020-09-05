using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems
{
    public class PostRollingStocksIntegrationTests : AbstractWebApplicationFixture
    {
        public PostRollingStocksIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task PostRollingStocks_ShouldReturn401Unauthorized_WhenTheUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var itemSlug = "acme-123456";
            var response = await client.PostJsonAsync($"/api/v1/catalogItems/{itemSlug}/rollingStocks", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PostRollingStocks_ShouldReturn404NotFound_WhenTheCatalogItemIsNotFound()
        {
            var client = CreateAuthorizedHttpClient();

            var itemSlug = "not-found";

            var request = new
            {
                epoch = "VI",
                category = "ElectricLocomotive",
                railway = "fs"
            };

            var response = await client.PostJsonAsync($"/api/v1/catalogItems/{itemSlug}/rollingStocks", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PostRollingStocks_ShouldReturn422UnprocessableEntity_WhenTheRailwayIsNotFound()
        {
            var client = CreateAuthorizedHttpClient();

            var itemSlug = "acme-60458";

            var request = new
            {
                epoch = "VI",
                category = "ElectricLocomotive",
                railway = "not-found"
            };

            var response = await client.PostJsonAsync($"/api/v1/catalogItems/{itemSlug}/rollingStocks", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task PostRollingStocks_ShouldReturn201Created_WhenTheRollingStockIsAddedToTheCatalogItem()
        {
            var client = CreateAuthorizedHttpClient();

            var itemSlug = "acme-60458";

            var request = new
            {
                epoch = "VI",
                category = "ElectricLocomotive",
                railway = "fs"
            };

            var response = await client.PostJsonAsync($"/api/v1/catalogItems/{itemSlug}/rollingStocks", request, Check.IsSuccessful);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            response.Headers.Should().NotBeEmpty();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/CatalogItems/acme-60458"));

            var content = await response.ExtractContent<PostCatalogItemResponse>();
            content.Slug.Value.Should().Be(Slug.Of("acme-60458"));
        }
    }
}
