using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Scales
{
    public class PutScalesIntegrationTests : AbstractWebApplicationFixture
    {
        public PutScalesIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task PutScales_ShouldReturn401Unauthorized_WhenTheUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var scale = "H0";
            var response = await client.PutJsonAsync($"/api/v1/scales/{scale}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PutScales_ShouldReturn400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = CreateAuthorizedHttpClient();

            var request = new
            {
                ratio = -87M
            };

            var scale = "H0";
            var response = await client.PutJsonAsync($"/api/v1/scales/{scale}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PutScales_ShouldReturn404NotFound_WhenScaleToEditWasNotFound()
        {
            var client = CreateAuthorizedHttpClient();

            var scale = "not-found";
            var response = await client.PutJsonAsync($"/api/v1/scales/{scale}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PutScales_ShouldReturn200OK_WhenScaleWasUpdated()
        {
            var client = CreateAuthorizedHttpClient();

            var scale = "H0";
            var request = new
            {
                ratio = 87M
            };
            var response = await client.PutJsonAsync($"/api/v1/scales/{scale}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
