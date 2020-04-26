using FluentAssertions;
using IntegrationTests;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class EditRailwayIntegrationTests : AbstractWebApplicationFixture
    {
        public EditRailwayIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task EditRailway_ShouldReturn401Unauthorized_WhenTheUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var railway = "db";
            var response = await client.PutJsonAsync($"/api/v1/railways/{railway}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task EditRailway_ShouldReturn400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var railway = "db";
            var request = new
            {
                Country = "ZZ"
            };

            var response = await client.PutJsonAsync($"/api/v1/railways/{railway}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task EditRailway_ShouldReturn404NotFound_WhenRailwayToEditWasNotFound()
        {
            var client = await CreateAuthorizedHttpClientAsync();

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
        public async Task EditRailway_ShouldReturn200OK_WhenRailwayWasUpdated()
        {
            var client = await CreateAuthorizedHttpClientAsync();

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
