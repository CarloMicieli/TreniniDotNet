using FluentAssertions;
using IntegrationTests;
using System;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class GetScaleBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        public GetScaleBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory) 
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

            content._Links.Should().NotBeNull();
            content._Links.Slug.Should().Be("h0");
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

        class GetScaleBySlugResponseLinks
        {
            public string Slug { set; get; }
            public string _Self { set; get; }
        }

        class GetScaleBySlugResponse
        {
            public Guid Id { set; get; }
            public GetScaleBySlugResponseLinks _Links { set; get; }
            public string Slug { set; get; }
            public string Name { set; get; }
            public decimal? Ratio { set; get; }
            public decimal? Gauge { set; get; }
            public string TrackGauge { set; get; }
        }
    }
}
