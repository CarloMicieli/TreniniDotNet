using FluentAssertions;
using IntegrationTests;
using System;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class GetRailwayBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        public GetRailwayBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetRailwayBySlug_ReturnsOk_WhenTheBrandExists()
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync("/api/v1/railways/fs");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await ExtractContent<GetRailwayBySlugResponse>(response);
            content.Name.Should().Be("FS");

            content._Links.Should().NotBeNull();
            content._Links.Slug.Should().Be("fs");
            content._Links._Self.Should().Be("http://localhost/api/v1/Railways/fs");
        }

        [Fact]
        public async Task GetRailwayBySlug_ReturnsNotFound_WhenTheBrandDoesNotExist()
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync("/api/v1/railways/not-found");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        class GetRailwayBySlugLinks
        {
            public string Slug { set; get; }
            public string _Self { set; get; }
        }

        class GetRailwayBySlugResponse
        {
            public Guid Id { set; get; }
            public GetRailwayBySlugLinks _Links { set; get; }
            public string Slug { set; get; }
            public string Name { set; get; }
            public string CompanyName { set; get; }
            public string Country { set; get; }
            public string Status { set; get; }
            public DateTime? OperatingSince { set; get; }
            public DateTime? OperatingUntil { set; get; }
        }
    }
}