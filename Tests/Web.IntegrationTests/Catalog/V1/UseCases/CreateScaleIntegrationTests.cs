using FluentAssertions;
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
        public async Task CreateScale_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var response = await client.PostJsonAsync("/api/v1/scales", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task CreateScale_ShouldReturn201Created_WhenTheNewScaleIsCreated()
        {
            var client = await CreateAuthorizedHttpClientAsync();

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

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/Scales/nn"));
        }

        [Fact]
        public async Task CreateScale_ShouldReturn400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var content = new
            {
                Gauge = new
                {
                    TrackGauge = "Standard",
                    Millimeters = -16.5M
                },
                Ratio = 87
            };

            var response = await client.PostJsonAsync("/api/v1/scales", content, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateScale_ShouldReturn409Conflict_WhenTheScaleAlreadyExist()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var content = new
            {
                Name = "H0",
                Gauge = new
                {
                    TrackGauge = "Standard",
                    Millimeters = 16.5M
                },
                Ratio = 87
            };

            var response = await client.PostJsonAsync("/api/v1/scales", content, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}
