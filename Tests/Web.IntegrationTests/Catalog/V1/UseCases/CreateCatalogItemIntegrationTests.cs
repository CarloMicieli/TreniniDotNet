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
    public class CreateCatalogItemIntegrationTests : AbstractWebApplicationFixture
    {
        public CreateCatalogItemIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var response = await client.PostJsonAsync("/api/v1/catalogItems", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldReturn400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var response = await client.PostJsonAsync("/api/v1/catalogItems", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldReturn422UnprocessableEntity_WhenScaleNotExist()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var content = new
            {
                brand = "Acme",
                itemNumber = "123456",
                description = "My new catalog item",
                powerMethod = "dc",
                scale = "---",
                rollingStocks = JsonArray(new
                {
                    epoch = "VI",
                    category = "ElectricLocomotive",
                    railway = "fs"
                })
            };

            var response = await client.PostJsonAsync("/api/v1/catalogItems", content, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldReturn422UnprocessableEntity_WhenBrandNotExist()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var content = new
            {
                brand = "NotFound",
                itemNumber = "123456",
                description = "My new catalog item",
                powerMethod = "dc",
                scale = "H0",
                rollingStocks = JsonArray(new
                {
                    epoch = "VI",
                    category = "ElectricLocomotive",
                    railway = "fs"
                })
            };

            var response = await client.PostJsonAsync("/api/v1/catalogItems", content, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldReturn422UnprocessableEntity_WhenRailwayNotExist()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var content = new
            {
                brand = "Acme",
                itemNumber = "123456",
                description = "My new catalog item",
                powerMethod = "dc",
                scale = "H0",
                rollingStocks = JsonArray(new
                {
                    epoch = "VI",
                    category = "ElectricLocomotive",
                    railway = "----"
                })
            };

            var response = await client.PostJsonAsync("/api/v1/catalogItems", content, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldReturn201Created_WhenCatalogItemIsCreated()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var content = new
            {
                brand = "Acme",
                itemNumber = "123456",
                description = "My new catalog item",
                prototypeDescription = (string)null,
                modelDescription = (string)null,
                powerMethod = "dc",
                scale = "H0",
                rollingStocks = JsonArray(new
                {
                    epoch = "VI",
                    category = "ElectricLocomotive",
                    railway = "fs",
                    length = new
                    {
                        Millimeters = 210
                    },
                    className = "",
                    roadNumber = ""
                })
            };

            var response = await client.PostJsonAsync("/api/v1/catalogItems", content, Check.IsSuccessful);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Should().NotBeEmpty();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/CatalogItems/acme-123456"));
        }
    }
}
