using FluentAssertions;
using IntegrationTests;
using System;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class CreateRailwayIntegrationTests : AbstractWebApplicationFixture
    {
        public CreateRailwayIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task CreateRailway_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var response = await client.PostJsonAsync("/api/v1/railways", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task CreateRailway_ShouldReturn201Created_WhenTheNewRailwayIsCreated()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var content = new
            {
                Name = "New Railway",
                CompanyName = "Ferrovie dello stato",
                Country = "IT",
                Status = "Active",
                OperatingSince = new DateTime(1905, 7, 1)
            };

            var response = await client.PostJsonAsync("/api/v1/railways", content, Check.IsSuccessful);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/Railways/new-railway"));
        }

        [Fact]
        public async Task CreateRailway_ShouldReturn400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var content = new
            {
                CompanyName = "Ferrovie dello stato",
                Country = "IT",
                Status = "Active",
                OperatingSince = new DateTime(1905, 7, 1)
            };

            var response = await client.PostJsonAsync("/api/v1/railways", content, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateRailway_ShouldReturn409Conflict_WhenTheRailwayAlreadyExist()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var content = new
            {
                Name = "FS",
                CompanyName = "Ferrovie dello stato",
                Country = "IT",
                Status = "Active",
                OperatingSince = new DateTime(1905, 7, 1)
            };

            var response = await client.PostJsonAsync("/api/v1/railways", content, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}
