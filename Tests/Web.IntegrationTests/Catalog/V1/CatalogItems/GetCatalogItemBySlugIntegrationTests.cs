using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems.Responses;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems
{
    public class GetCatalogItemBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        public GetCatalogItemBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetCatalogItemBySlug_ShouldReturn200OK_WhenTheBrandExists()
        {
            var content = await GetJsonAsync<CatalogItemResponse>("/api/v1/catalogitems/acme-60392");

            content.Should().NotBeNull();
            //content.Id.Should().Be(new Guid("9ed9f089-2053-4a39-b669-a6d603080402"));
            content._Links.Should().NotBeNull();
            content._Links.Slug.Should().Be("acme-60392");
            content._Links._Self.Should().Be("http://localhost/api/v1/CatalogItems/acme-60392");
        }

        [Fact]
        public async Task GetCatalogItemBySlug_ShouldReturn404NotFound_WhenTheBrandDoesNotExist()
        {
            var response = await GetAsync("/api/v1/catalogitems/not-found");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
