using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems
{
    public class GetCatalogItemBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        private readonly HttpClient _client;

        public GetCatalogItemBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCatalogItemBySlug_ShouldReturn200OK_WhenCatalogItemsExist()
        {
            var content = await _client.GetJsonAsync<CatalogItemResponse>("/api/v1/catalogitems/acme-60392");

            content.Should().NotBeNull();
            //content.Id.Should().Be(new Guid("9ed9f089-2053-4a39-b669-a6d603080402"));
            content._Links.Should().NotBeNull();
            content._Links.Slug.Should().Be("acme-60392");
            content._Links._Self.Should().Be("http://localhost/api/v1/CatalogItems/acme-60392");
        }

        [Fact]
        public async Task GetCatalogItemBySlug_ShouldReturn404NotFound_WhenTheBrandDoesNotExist()
        {
            var response = await _client.GetAsync("/api/v1/catalogitems/not-found");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
