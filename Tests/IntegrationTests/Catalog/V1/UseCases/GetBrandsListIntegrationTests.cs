using FluentAssertions;
using IntegrationTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public sealed class GetBrandsListIntegrationTests : AbstractWebApplicationFixture
    {
        public GetBrandsListIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetBrandsList_ShouldReturnTheBrands()
        {
            var content = await GetJsonAsync<GetBrandsListResponse>("/api/v1/brands");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be("http://localhost/api/v1/Brands?start=0&limit=50");

            content.Results.Should().NotBeEmpty();

            var firstResult = content.Results.First();
            firstResult._Links.Should().NotBeNull();
            firstResult._Links._Self.Should().Be("http://localhost/api/v1/Brands/acme");
            firstResult._Links.Slug.Should().Be("acme");
        }

        [Fact]
        public async Task GetBrandsList_ShouldReturnTheFirstPageOfBrands()
        {
            var limit = 2;
            var content = await GetJsonAsync<GetBrandsListResponse>($"/api/v1/brands?start=0&limit={limit}");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be($"http://localhost/api/v1/Brands?start=0&limit={limit}");
            content._links.Prev.Should().BeNull();
            content._links.Next.Should().Be($"http://localhost/api/v1/Brands?start={limit}&limit={limit}");

            content.Limit.Should().Be(limit);

            content.Results.Should().NotBeEmpty();
            content.Results.Should().HaveCount(limit);
        }

        [Fact]
        public async Task GetBrandsList_ShouldReturnAPageOfBrands()
        {
            var limit = 2;
            var content = await GetJsonAsync<GetBrandsListResponse>($"/api/v1/brands?start=2&limit={limit}");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be($"http://localhost/api/v1/Brands?start=2&limit={limit}");
            content._links.Prev.Should().Be($"http://localhost/api/v1/Brands?start=0&limit={limit}");
            content._links.Next.Should().Be($"http://localhost/api/v1/Brands?start=4&limit={limit}");

            content.Limit.Should().Be(limit);

            content.Results.Should().NotBeEmpty();
            content.Results.Should().HaveCount(limit);
        }

        class GetBrandsListLinks
        {
            public string _Self { set; get; }
            public string Prev { set; get; }
            public string Next { set; get; }
        }

        class GetBrandsListResponse
        {
            public GetBrandsListLinks _links { get; set; }
            public int? Limit { get; set; }
            public List<GetBrandsListElement> Results { get; set; }
        }

        class GetBrandsListElementLinks
        {
            public string Slug { set; get; }
            public string _Self { set; get; }
        }

        class GetBrandsListElement
        {
            public GetBrandsListElementLinks _Links { set; get; }
            public Guid BrandId { set; get; }
            public string Name { get; set; }
            public string CompanyName { get; set; }
            public string MailAddress { get; set; }
            public string WebsiteUrl { get; set; }
            public string Kind { get; set; }
            public bool? Active { get; set; }
        }
    }
}
