using IntegrationTests;
using System;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class CreateScaleIntegrationTests : AbstractWebApplicationFixture
    {
        public CreateScaleIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task CreateNewScales_ReturnsOk()
        {
            var client = CreateHttpClient();
            var content = new
            {
                Name = "NN",
                Gauge = new
                {
                    TrackGauge = "Standard",
                    Millimeters = 16.5M
                },
                Ratio = 87
            };

            var response = await client.PostJsonAsync("/api/v1/scales", content, Check.IsSuccessful);

            Assert.NotNull(response.Headers.Location);
            Assert.Equal(new Uri("http://localhost/api/v1/Scales/nn"), response.Headers.Location);
        }

        [Fact]
        public async Task CreateNewScales_ReturnsError_WhenTheRequestIsInvalid()
        {
            var client = CreateHttpClient();
            var content = JsonContent(new
            {
                Gauge = 16.5,
                Ratio = 87,
                TrackGauge = "standard"
            });

            var response = await client.PostAsync("/api/v1/scales", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateNewScales_ReturnsBadRequest_WhenTheScaleAlreadyExist()
        {
            var client = CreateHttpClient();
            var content = JsonContent(new
            {
                Name = "H0",
                Gauge = 16.5,
                Ratio = 87,
                TrackGauge = "standard"
            });

            var response = await client.PostAsync("/api/v1/scales", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
