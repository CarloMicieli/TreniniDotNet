using FluentAssertions;
using IntegrationTests;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Catalog.V1.Responses;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class GetRailwayBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        public GetRailwayBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldReturn200OK_WhenTheBrandExists()
        {
            var content = await GetJsonAsync<RailwayResponse>("/api/v1/railways/fs");

            content.Name.Should().Be("FS");
            content._Links.Should().NotBeNull();
            content._Links.Slug.Should().Be("fs");
            content._Links._Self.Should().Be("http://localhost/api/v1/Railways/fs");
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldReturn404NotFound_WhenTheBrandDoesNotExist()
        {
            var response = await GetAsync("/api/v1/railways/not-found");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}