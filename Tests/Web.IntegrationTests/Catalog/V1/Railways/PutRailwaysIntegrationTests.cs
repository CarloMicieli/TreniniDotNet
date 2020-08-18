using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Railways
{
    public class PutRailwaysIntegrationTests : AbstractWebApplicationFixture
    {
        public PutRailwaysIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task PutRailways_ShouldReturn401Unauthorized_WhenTheUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var railway = "db";
            var response = await client.PutJsonAsync($"/api/v1/railways/{railway}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PutRailways_ShouldReturn400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = CreateAuthorizedHttpClient();

            var railway = "db";
            var request = new
            {
                Country = "ZZ"
            };

            var response = await client.PutJsonAsync($"/api/v1/railways/{railway}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PutRailways_ShouldReturn404NotFound_WhenRailwayToEditWasNotFound()
        {
            var client = CreateAuthorizedHttpClient();

            var railway = "not-found";
            var request = new
            {
                CompanyName = "Ferrovie dello stato",
                Country = "IT"
            };

            var response = await client.PutJsonAsync($"/api/v1/railways/{railway}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PutRailways_ShouldReturn200OK_WhenRailwayWasUpdated()
        {
            var client = CreateAuthorizedHttpClient();

            var railway = "db";
            var request = new
            {
                CompanyName = "Ferrovie dello stato",
                Country = "IT"
            };

            var response = await client.PutJsonAsync($"/api/v1/railways/{railway}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
