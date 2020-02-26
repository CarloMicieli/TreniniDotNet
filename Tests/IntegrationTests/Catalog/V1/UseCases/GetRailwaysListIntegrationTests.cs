using IntegrationTests;
using System;
using System.Collections.Generic;
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
        public async Task GetRailwaysList_ShouldReturnTheBrands()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/railways");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await ExtractContent<GetRailwaysListResponse>(response);
            Assert.True(content.Results.Count > 0);
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
