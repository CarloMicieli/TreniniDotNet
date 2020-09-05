using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems
{
    public class GetLatestCatalogItemsIntegrationTests : AbstractWebApplicationFixture
    {
        private HttpClient _client;

        public GetLatestCatalogItemsIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetLatestCatalogItems_ShouldReturn200OK_WhenTheBrandExists()
        {
            var limit = 2;
            var content = await _client.GetJsonAsync<CatalogItemsListResponse>($"/api/v1/CatalogItems/latest?start=2&limit={limit}");

            content.Should().NotBeNull();
            content._links.Should().NotBeNull();
            content._links._Self.Should().Be($"http://localhost/api/v1/CatalogItems/latest?start=2&limit={limit}");
            content._links.Prev.Should().Be($"http://localhost/api/v1/CatalogItems/latest?start=0&limit={limit}");
            content._links.Next.Should().Be($"http://localhost/api/v1/CatalogItems/latest?start=4&limit={limit}");

            content.Limit.Should().Be(limit);

            content.Results.Should().NotBeEmpty();
            content.Results.Should().HaveCount(limit);
        }
    }
}
