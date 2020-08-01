using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Catalog.V1.Scales.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Scales
{
    public class GetScaleBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        private HttpClient _client;

        public GetScaleBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetScaleBySlug_ShouldReturn200OK_WhenTheScaleExists()
        {
            var scale = CatalogSeedData.Scales.ScaleH0();

            var content = await _client.GetJsonAsync<ScaleResponse>($"/api/v1/scales/{scale.Slug.Value}");

            content._Links.Should().NotBeNull();
            content._Links.Slug.Should().Be("h0");
            content._Links._Self.Should().Be("http://localhost/api/v1/Scales/h0");

            content.Id.Should().Be(scale.Id.ToGuid());
            content.Name.Should().Be("H0");
        }

        [Fact]
        public async Task GetScaleBySlug_ShouldReturn404NotFound_WhenTheScaleDoesNotExist()
        {
            var response = await _client.GetAsync("/api/v1/scales/not-found");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
