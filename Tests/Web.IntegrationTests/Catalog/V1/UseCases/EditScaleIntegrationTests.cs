using FluentAssertions;
using IntegrationTests;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class EditScaleIntegrationTests : AbstractWebApplicationFixture
    {
        public EditScaleIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task EditScale_ShouldReturn401Unauthorized_WhenTheUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var request = new
            {
            };

            var scale = "H0";
            var response = await client.PutJsonAsync($"/api/v1/scales/{scale}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task EditScale_ShouldReturn400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var request = new
            {
                Ratio = -87M
            };

            var scale = "H0";
            var response = await client.PutJsonAsync($"/api/v1/scales/{scale}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task EditScale_ShouldReturn404NotFound_WhenScaleToEditWasNotFound()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var scale = "not-found";
            var response = await client.PutJsonAsync($"/api/v1/scales/{scale}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EditScale_ShouldReturn200OK_WhenScaleWasUpdated()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var scale = "H0";
            var request = new
            {
                Ratio = 87M
            };
            var response = await client.PutJsonAsync($"/api/v1/scales/{scale}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
