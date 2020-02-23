using IntegrationTests;
using System;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class CreateScaleTests : AbstractWebApplicationFixture
    {
        public CreateScaleTests(CustomWebApplicationFactory<Startup> factory) 
            : base(factory)
        {
        }

        [Fact]
        public async Task CreateNewScales_ReturnsOk()
        {
            // Arrange
            var client = CreateHttpClient();
            var content = JsonContent(new
            {
                Name = "NN",
                Gauge = 16.5,
                Ratio = 87,
                TrackGauge = "standard"
            });

            // Act
            var response = await client.PostAsync("/api/v1/scales", content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.NotNull(response.Headers.Location);
            Assert.Equal(new Uri("http://localhost/api/v1/Scales/nn"), response.Headers.Location);
        }

        [Fact]
        public async Task CreateNewScales_ReturnsError_WhenTheRequestIsInvalid()
        {
            // Arrange
            var client = CreateHttpClient();
            var content = JsonContent(new
            {
                Gauge = 16.5,
                Ratio = 87,
                TrackGauge = "standard"
            });

            // Act
            var response = await client.PostAsync("/api/v1/scales", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateNewScales_ReturnsBadRequest_WhenTheScaleAlreadyExist()
        {
            // Arrange
            var client = CreateHttpClient();
            var content = JsonContent(new
            {
                Name = "H0",
                Gauge = 16.5,
                Ratio = 87,
                TrackGauge = "standard"
            });

            // Act
            var response = await client.PostAsync("/api/v1/scales", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
