using FluentAssertions;
using IntegrationTests;
using System;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Catalog.V1.Responses;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class GetBrandBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        public GetBrandBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetBrandBySlug_ShouldReturn200OK_WhenTheBrandExists()
        {
            var content = await GetJsonAsync<BrandResponse>("/api/v1/brands/acme");

            content.Should().NotBeNull();
            content.Id.Should().Be(new Guid("9ed9f089-2053-4a39-b669-a6d603080402"));
            content.Name.Should().Be("ACME");
            content._Links.Should().NotBeNull();
            content._Links.Slug.Should().Be("acme");
            content._Links._Self.Should().Be("http://localhost/api/v1/Brands/acme");
        }

        [Fact]
        public async Task GetBrandBySlug_ShouldReturn404NotFound_WhenTheBrandDoesNotExist()
        {
            var response = await GetAsync("/api/v1/brands/not-found");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}