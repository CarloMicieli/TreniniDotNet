using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TreniniDotNet.IntegrationTests.Catalog.V1.Brands.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Brands
{
    public sealed class GetBrandBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        private HttpClient _client;

        public GetBrandBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/v1/brands/");
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetBrandBySlug_ShouldReturn200OK_WhenTheBrandExists()
        {
            var content = await _client.GetJsonAsync<BrandResponse>("acme");

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
            var response = await _client.GetAsync("not-found");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}