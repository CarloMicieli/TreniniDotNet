using FluentAssertions;
using IntegrationTests;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class EditBrandIntegrationTests : AbstractWebApplicationFixture
    {
        public EditBrandIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task EditBrand_Returns401Unauthorized_WhenTheUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var request = new
            {
            };

            var brand = "acme";
            var response = await client.PutJsonAsync($"/api/v1/brands/{brand}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task EditBrand_Returns400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var request = new
            {
                BrandType = "--invalid--"
            };

            var brand = "acme";
            var response = await client.PutJsonAsync($"/api/v1/brands/{brand}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task EditBrand_Returns404NotFound_WhenBrandToEditWasNotFound()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var brand = "not-found";
            var response = await client.PutJsonAsync($"/api/v1/brands/{brand}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EditBrand_Returns200OK_WhenBrandWasUpdated()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var brand = "acme";
            var request = new
            {
                CompanyName = "Associazione Costruzioni Modellistiche Esatte",
            };
            var response = await client.PutJsonAsync($"/api/v1/brands/{brand}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
