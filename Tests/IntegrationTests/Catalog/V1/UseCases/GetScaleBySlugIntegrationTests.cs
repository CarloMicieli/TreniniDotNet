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
            var client = CreateHttpClient();

            var response = await client.GetAsync("/api/v1/scales/h0");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await ExtractContent<GetScaleBySlugResponse>(response);

            content._Links.Should().NotBeNull();
            content._Links.Slug.Should().Be("h0");
            content._Links._Self.Should().Be("http://localhost/api/v1/Scales/h0");

            content.Id.Should().Be(new Guid("7edfb586-218c-4997-8820-f61d3a81ce66"));
            content.Name.Should().Be("H0");
        }

        [Fact]
        public async Task GetScaleBySlug_ReturnsNotFound_WhenTheScaleDoesNotExist()
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync("/api/v1/scales/not-found");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
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
