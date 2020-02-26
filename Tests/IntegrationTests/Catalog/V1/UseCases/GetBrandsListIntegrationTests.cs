using FluentAssertions;
using IntegrationTests;
using System;
using System.Collections.Generic;
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
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/brands");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await ExtractContent<GetBrandsListResponse>(response);

            content.Results.Should().NotBeEmpty();
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
