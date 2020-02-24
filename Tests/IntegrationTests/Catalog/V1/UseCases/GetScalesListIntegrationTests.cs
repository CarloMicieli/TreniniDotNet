using IntegrationTests;
using System.Collections.Generic;
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
        public async Task GetRailwaysList_ShouldReturnTheBrands()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/scales");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await ExtractContent<List<GeScalesList>>(response);
            Assert.True(content.Count > 0);
        }
    }

    class GeScalesList
    {
        public string Slug { get; set; }
    }
}
