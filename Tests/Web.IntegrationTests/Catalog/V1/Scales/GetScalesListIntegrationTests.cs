﻿using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Catalog.V1.Scales.Responses;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Scales
{
    public sealed class GetScalesListIntegrationTests : AbstractWebApplicationFixture
    {
        public GetScalesListIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetScalesList_ShouldReturn200OK_AndTheScalesList()
        {
            var content = await GetJsonAsync<ScalesListResponse>("/api/v1/scales");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be("http://localhost/api/v1/Scales?start=0&limit=50");

            content.Results.Should().NotBeEmpty();

            var first = content.Results.First();
            first.Should().NotBeNull();
            first._Links.Should().NotBeNull();
            first._Links._Self.Should().Be("http://localhost/api/v1/Scales/0");
            first._Links.Slug.Should().Be("0");
        }

        [Fact]
        public async Task GetScalesList_ShouldReturn200OK_AndTheFirstPageOfScales()
        {
            var limit = 2;
            var content = await GetJsonAsync<ScalesListResponse>($"/api/v1/scales?start=0&limit={limit}");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be($"http://localhost/api/v1/Scales?start=0&limit={limit}");
            content._links.Prev.Should().BeNull();
            content._links.Next.Should().Be($"http://localhost/api/v1/Scales?start=2&limit={limit}");

            content.Results.Should().NotBeEmpty();
            content.Results.Should().HaveCount(limit);

            var first = content.Results.First();
            first.Should().NotBeNull();
            first._Links.Should().NotBeNull();
            first._Links._Self.Should().Be("http://localhost/api/v1/Scales/0");
            first._Links.Slug.Should().Be("0");
        }

        [Fact]
        public async Task GetScalesList_ShouldReturn200OK_AndTheScalesWithPagination()
        {
            var limit = 2;
            var content = await GetJsonAsync<ScalesListResponse>($"/api/v1/scales?start=2&limit={limit}");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be($"http://localhost/api/v1/Scales?start=2&limit={limit}");
            content._links.Prev.Should().Be($"http://localhost/api/v1/Scales?start=0&limit={limit}");
            content._links.Next.Should().Be($"http://localhost/api/v1/Scales?start=4&limit={limit}");

            content.Limit.Should().Be(limit);

            content.Results.Should().NotBeEmpty();
            content.Results.Should().HaveCount(limit);

            var first = content.Results.First();
            first.Should().NotBeNull();
            first._Links.Should().NotBeNull();
            first._Links._Self.Should().Be("http://localhost/api/v1/Scales/h0");
            first._Links.Slug.Should().Be("h0");
        }
    }
}