using IntegrationTests;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.Web;
using TreniniDotNet.Web.ViewModels.Links;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class GetBrandBySlugTests : AbstractWebApplicationFixture
    {
        public GetBrandBySlugTests(CustomWebApplicationFactory<Startup> factory) 
            : base(factory)
        {
        }

        [Fact]
        public async Task GetBrandBySlug_ReturnsOk_WhenTheBrandExists()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/brands/acme");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await ExtractContent<Response>(response);
            Assert.Equal("acme", content._Links.Slug);
        }

        [Fact]
        public async Task GetBrandBySlug_ReturnsNotFound_WhenTheBrandDoesNotExist()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/brands/not-found");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    class Response
    {
        public LinksView _Links { set; get; }
        public string Name { set; get; }
    }
}