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
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/railways/fs");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var responseContent = await ExtractContent<GetRailwayBySlugResponse>(response);
            responseContent.Name.Should().Be("FS");

            responseContent._Links.Should().NotBeNull();
            responseContent._Links.Slug.Should().Be("fs");
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