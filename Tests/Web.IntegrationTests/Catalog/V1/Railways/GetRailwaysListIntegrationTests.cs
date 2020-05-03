using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Catalog.V1.Railways.Responses;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Railways
{
    public sealed class GetRailwaysListIntegrationTests : AbstractWebApplicationFixture
    {
        public GetRailwaysListIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetRailwaysList_ShouldReturn200OK_AndTheRailways()
        {
            var content = await GetJsonAsync<RailwaysListResponse>("/api/v1/railways");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be("http://localhost/api/v1/Railways?start=0&limit=50");

            content.Results.Should().NotBeEmpty();

            var first = content.Results.First();
            first.Should().NotBeNull();
            first._Links.Should().NotBeNull();
            first._Links.Slug.Should().Be("db");
            first._Links._Self.Should().Be("http://localhost/api/v1/Railways/db");
        }

        [Fact]
        public async Task GetRailwaysList_ShouldReturn200OK_AndTheFirstPageOfRailways()
        {
            var limit = 2;
            var content = await GetJsonAsync<RailwaysListResponse>($"/api/v1/railways?start=0&limit={limit}");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be($"http://localhost/api/v1/Railways?start=0&limit={limit}");
            content._links.Next.Should().Be($"http://localhost/api/v1/Railways?start=2&limit={limit}");
            content._links.Prev.Should().BeNull();

            content.Results.Should().NotBeEmpty();
            content.Results.Should().HaveCount(limit);
        }

        [Fact]
        public async Task GetRailwaysList_ShouldReturn200OK_AndTheRailwaysWithPagination()
        {
            var limit = 2;
            var content = await GetJsonAsync<RailwaysListResponse>($"/api/v1/railways?start=2&limit={limit}");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be($"http://localhost/api/v1/Railways?start=2&limit={limit}");
            content._links.Next.Should().Be($"http://localhost/api/v1/Railways?start=4&limit={limit}");
            content._links.Prev.Should().Be($"http://localhost/api/v1/Railways?start=0&limit={limit}");

            content.Results.Should().NotBeEmpty();
            content.Results.Should().HaveCount(limit);
        }
    }
}
