using IntegrationTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public sealed class GetScalesListIntegrationTests : AbstractWebApplicationFixture
    {
        public GetScalesListIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetScalesList_ShouldReturnTheBrands()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/scales");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await ExtractContent<GetScalesListResponse>(response);
            Assert.True(content.Results.Count() > 0);
        }

        class GetScalesListLinks
        {
            public string _Self { set; get; }
            public string Prev { set; get; }
            public string Next { set; get; }
        }

        class GetScalesListResponse
        {
            public GetScalesListLinks _links { get; set; }
            public int? Limit { get; set; }
            public List<GetScalesListElement> Results { set; get; }
        }

        class GetScalesListElementLinks
        {
            public string Slug { set; get; }
            public string _Self { set; get; }
        }

        class GetScalesListElement
        {
            public Guid Id { set; get; }
            public GetScalesListElementLinks _Links { set; get; }
            public string Slug { set; get; }
            public string Name { set; get; }
            public decimal? Ratio { set; get; }
            public decimal? Gauge { set; get; }
            public string TrackGauge { set; get; }
        }
    }
}
