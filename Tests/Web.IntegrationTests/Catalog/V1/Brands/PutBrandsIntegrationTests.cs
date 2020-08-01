using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Brands
{
    public class PutBrandsIntegrationTests : AbstractWebApplicationFixture
    {
        public PutBrandsIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task PutBrands_ShouldReturn401Unauthorized_WhenTheUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var request = new
            {
            };

            const string brand = "acme";
            var response = await client.PutJsonAsync($"/api/v1/brands/{brand}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PutBrands_ShouldReturn400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var request = new
            {
                BrandType = "--invalid--"
            };

            const string brand = "acme";
            var response = await client.PutJsonAsync($"/api/v1/brands/{brand}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PutBrands_ShouldReturn404NotFound_WhenBrandToEditWasNotFound()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            const string brand = "not-found";
            var response = await client.PutJsonAsync($"/api/v1/brands/{brand}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PutBrands_ShouldReturn200OK_WhenBrandWasUpdated()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            const string brand = "acme";
            var request = new
            {
                CompanyName = "Associazione Costruzioni Modellistiche Esatte",
            };
            var response = await client.PutJsonAsync($"/api/v1/brands/{brand}", request, Check.IsSuccessful);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
