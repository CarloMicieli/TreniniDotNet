using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Catalog.V1.Responses;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class GetLatestCatalogItemsIntegrationTests : AbstractWebApplicationFixture
    {
        public GetLatestCatalogItemsIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetLatestCatalogItems_ShouldReturn200OK_WhenTheBrandExists()
        {
            var limit = 2;
            var content = await GetJsonAsync<CatalogItemsListResponse>($"/api/v1/CatalogItems/latest?start=2&limit={limit}");

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
