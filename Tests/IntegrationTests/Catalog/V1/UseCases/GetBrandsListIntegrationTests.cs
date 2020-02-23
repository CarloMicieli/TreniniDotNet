using IntegrationTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public sealed class GetBrandsListIntegrationTests : AbstractWebApplicationFixture
    {
        public GetBrandsListIntegrationTests(CustomWebApplicationFactory<Startup> factory) 
            : base(factory)
        {
        }

        [Fact]
        public async Task GetBrandsList_ShouldReturnTheBrands()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/v1/brands");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await ExtractContent<List<GetBrandsListResponse>>(response);
            Assert.True(content.Count > 0);
        }
    }

    class GetBrandsListResponse 
    {
        public string Slug { get; set; }
    }
}
