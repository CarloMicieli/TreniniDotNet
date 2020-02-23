using IntegrationTests;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class GetRailwayBySlugTests : AbstractWebApplicationFixture
    {
        public GetRailwayBySlugTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetRailwayBySlug_ReturnsOk_WhenTheBrandExists()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/railways/fs");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await ExtractContent<GetRailwayBySlugResponse>(response);
            Assert.Equal("fs", content.Slug);
        }

        [Fact]
        public async Task GetRailwayBySlug_ReturnsNotFound_WhenTheBrandDoesNotExist()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/railways/not-found");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    class GetRailwayBySlugResponse
    {
        public string Slug { set; get; }
    }
}