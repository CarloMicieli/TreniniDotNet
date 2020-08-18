using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Catalog.V1.Scales.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Scales
{
    public class PostScalesIntegrationTests : AbstractWebApplicationFixture
    {
        public PostScalesIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task PostScales_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var response = await client.PostJsonAsync("/api/v1/scales", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PostScales_ShouldReturn201Created_WhenTheNewScaleIsCreated()
        {
            var client = CreateAuthorizedHttpClient();

            var model = new
            {
                name = "NN",
                gauge = new
                {
                    trackGauge = "Standard",
                    millimeters = 16.5M
                },
                ratio = 87,
                description = "New scale description",
                weight = 100,
                standards = new List<string>()
            };

            var response = await client.PostJsonAsync("/api/v1/scales", model, Check.IsSuccessful);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/Scales/nn"));

            var content = await response.ExtractContent<PostScaleResponse>();
            content.Slug.Value.Should().Be(Slug.Of("nn"));
        }

        [Fact]
        public async Task PostScales_ShouldReturn400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = CreateAuthorizedHttpClient();

            var content = new
            {
                gauge = new
                {
                    trackGauge = "Standard",
                    millimeters = -16.5M
                },
                ratio = 87
            };

            var response = await client.PostJsonAsync("/api/v1/scales", content, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PostScales_ShouldReturn409Conflict_WhenTheScaleAlreadyExist()
        {
            var client = CreateAuthorizedHttpClient();

            var content = new
            {
                name = "H0",
                gauge = new
                {
                    trackGauge = "Standard",
                    millimeters = 16.5M
                },
                ratio = 87
            };

            var response = await client.PostJsonAsync("/api/v1/scales", content, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}
