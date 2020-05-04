using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.Common;
using TreniniDotNet.IntegrationTests.Catalog.V1.Railways.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Railways
{
    public class PostRailwaysIntegrationTests : AbstractWebApplicationFixture
    {
        public PostRailwaysIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task PostRailways_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var response = await client.PostJsonAsync("/api/v1/railways", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PostRailways_ShouldReturn201Created_WhenTheNewRailwayIsCreated()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var model = new
            {
                name = "New Railway",
                companyName = "A new railway company",
                country = "CH",
                periodOfActivity = new
                {
                    status = "Active",
                    operatingSince = new DateTime(1905, 7, 1)
                },
                totalLength = new
                {
                    trackGauge = "Standard",
                    millimeters = 1435
                },
                gauge = new
                {
                    kilometers = 12345
                },
                websiteUrl = "https://www.newrailway.com",
                headquarters = "Chur (CH)"
            };

            var response = await client.PostJsonAsync("/api/v1/railways", model, Check.IsSuccessful);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/Railways/new-railway"));

            var content = await response.ExtractContent<PostRailwayResponse>();
            content.Should().NotBeNull();
            content.Slug.Value.Should().Be(Slug.Of("new-railway"));
        }

        [Fact]
        public async Task PostRailways_ShouldReturn400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var contentWithoutName = new
            {
                companyName = "Ferrovie dello stato",
                country = "IT",
                periodOfActivity = new
                {
                    status = "Active",
                    operatingSince = new DateTime(1905, 7, 1)
                }
            };

            var response = await client.PostJsonAsync("/api/v1/railways", contentWithoutName, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PostRailways_ShouldReturn409Conflict_WhenTheRailwayAlreadyExist()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var content = new
            {
                name = "FS",
                companyName = "Ferrovie dello stato",
                country = "IT",
                periodOfActivity = new
                {
                    status = "Active",
                    operatingSince = new DateTime(1905, 7, 1)
                }
            };

            var response = await client.PostJsonAsync("/api/v1/railways", content, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}
