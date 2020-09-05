using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Catalog.V1.Railways.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Railways
{
    public class GetRailwayBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        private HttpClient _client;

        public GetRailwayBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldReturn200OK_WhenTheBrandExists()
        {
            var content = await _client.GetJsonAsync<RailwayResponse>("/api/v1/railways/fs");

            content.Name.Should().Be("FS");
            content._Links.Should().NotBeNull();
            content._Links.Slug.Should().Be("fs");
            content._Links._Self.Should().Be("http://localhost/api/v1/Railways/fs");
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldReturn404NotFound_WhenTheBrandDoesNotExist()
        {
            var response = await _client.GetAsync("/api/v1/railways/not-found");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
