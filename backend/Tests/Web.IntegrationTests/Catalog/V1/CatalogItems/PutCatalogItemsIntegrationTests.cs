using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;
using Xunit.Abstractions;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems
{
    public class PutCatalogItemsIntegrationTests : AbstractWebApplicationFixture
    {
        public PutCatalogItemsIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task PutCatalogItems_ShouldReturn401Unauthorized_WhenTheUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var itemSlug = "acme-123456";
            var response = await client.PutJsonAsync($"/api/v1/catalogItems/{itemSlug}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PutCatalogItems_ShouldReturn404NotFound_WhenTheItemIsNotFound()
        {
            var client = CreateAuthorizedHttpClient();

            var itemSlug = "not-found";
            var response = await client.PutJsonAsync($"/api/v1/catalogItems/{itemSlug}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PutCatalogItems_ShouldReturn409Conflict_WhenTheBrandIsNotFound()
        {
            var client = CreateAuthorizedHttpClient();

            var itemSlug = "acme-60458";

            var request = new
            {
                description = "Modified",
                brand = "invalid"
            };

            var response = await client.PutJsonAsync($"/api/v1/catalogItems/{itemSlug}", request, Check.Nothing);
            Log("Response: {0}", response);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task PutCatalogItems_ShouldReturn409Conflict_WhenTheScaleIsNotFound()
        {
            var client = CreateAuthorizedHttpClient();

            var itemSlug = "acme-60458";

            var request = new
            {
                scale = "abc"
            };

            var response = await client.PutJsonAsync($"/api/v1/catalogItems/{itemSlug}", request, Check.Nothing);
            Log("Response: {0}", response);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task PutCatalogItems_ShouldReturn200OK_WhenTheScaleIsUpdated()
        {
            var client = CreateAuthorizedHttpClient();

            var itemSlug = "acme-60458";

            var request = new
            {
                description = "modified"
            };

            var response = await client.PutJsonAsync($"/api/v1/catalogItems/{itemSlug}", request, Check.Nothing);
            Log("Response: {0}", response);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var response2 = await client.GetAsync($"/api/v1/catalogItems/{itemSlug}");
            response2.EnsureSuccessStatusCode();
        }
    }
}
