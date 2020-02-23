using IntegrationTests;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class GetScaleBySlugTests : AbstractWebApplicationFixture
    {
        public GetScaleBySlugTests(CustomWebApplicationFactory<Startup> factory) 
            : base(factory)
        {
        }

        [Fact]
        public async Task GetScaleBySlug_ReturnsOk_WhenTheScaleExists()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/scales/h0");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await ExtractContent<GetScaleBySlugResponse>(response);
            Assert.Equal("h0", content.Slug);
        }

        [Fact]
        public async Task GetScaleBySlug_ReturnsNotFound_WhenTheScaleDoesNotExist()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/scales/not-found");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    class GetScaleBySlugResponse
    {
        public string Slug { get; set; }
    }
}
