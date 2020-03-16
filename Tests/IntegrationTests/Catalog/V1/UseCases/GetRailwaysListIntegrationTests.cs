using FluentAssertions;
using IntegrationTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public sealed class GetRailwaysListIntegrationTests : AbstractWebApplicationFixture
    {
        public GetRailwaysListIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetRailwaysList_ShouldReturnTheRailways()
        {
            var content = await GetJsonAsync<GetRailwaysListResponse>("/api/v1/railways");

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
        public async Task GetRailwaysList_ShouldReturnTheFirstPageOfRailways()
        {
            var limit = 2;
            var content = await GetJsonAsync<GetRailwaysListResponse>($"/api/v1/railways?start=0&limit={limit}");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be($"http://localhost/api/v1/Railways?start=0&limit={limit}");
            content._links.Next.Should().Be($"http://localhost/api/v1/Railways?start=2&limit={limit}");
            content._links.Prev.Should().BeNull();

            content.Results.Should().NotBeEmpty();
            content.Results.Should().HaveCount(limit);
        }

        [Fact]
        public async Task GetRailwaysList_ShouldReturnTheRailways_WithPagination()
        {
            var limit = 2;
            var content = await GetJsonAsync<GetRailwaysListResponse>($"/api/v1/railways?start=2&limit={limit}");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be($"http://localhost/api/v1/Railways?start=2&limit={limit}");
            content._links.Next.Should().Be($"http://localhost/api/v1/Railways?start=4&limit={limit}");
            content._links.Prev.Should().Be($"http://localhost/api/v1/Railways?start=0&limit={limit}");

            content.Results.Should().NotBeEmpty();
            content.Results.Should().HaveCount(limit);
        }

        class GetRailwaysListLinks
        {
            public string _Self { set; get; }
            public string Prev { set; get; }
            public string Next { set; get; }
        }

        class GetRailwaysListResponse
        {
            public GetRailwaysListLinks _links { get; set; }
            public int? Limit { get; set; }
            public List<GetRailwaysListElement> Results { set; get; }
        }

        class GetRailwaysListElementLinks
        {
            public string Slug { set; get; }
            public string _Self { set; get; }
        }

        class GetRailwaysListElement
        {
            public Guid Id { set; get; }
            public GetRailwaysListElementLinks _Links { set; get; }
            public string Slug { set; get; }
            public string Name { set; get; }
            public string CompanyName { set; get; }
            public string Country { set; get; }
            public string Status { set; get; }
            public DateTime? OperatingSince { set; get; }
            public DateTime? OperatingUntil { set; get; }
        }
    }
}
