using FluentAssertions;
using IntegrationTests;
using System;
using System.Collections.Generic;
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
        public async Task CreateCatalogItem_ReturnsError_WhenTheRequestIsInvalid()
        {
            var client = CreateHttpClient();

            var response = await client.PostJsonAsync("/api/v1/catalogItems", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateCatalogItem_ReturnsOk()
        {
            var client = CreateHttpClient();

            var content = new
            {
                brandName = "ACME", //TODO: case sensitive now!
                itemNumber = "60458",
                description = "My new catalog item",
                prototypeDescription = (string)null,
                modelDescription = (string)null,
                powerMethod = "dc",
                scale = "H0", //TODO: case sensitive now!
                rollingStocks = JsonArray(new
                {
                    era = "VI",
                    category = "ElectricLocomotive",
                    railway = "FS", //TODO: case sensitive now!
                    length = 210,
                    className = "",
                    roadNumber = ""
                })
            };

            var response = await client.PostJsonAsync("/api/v1/catalogItems", content, Check.IsSuccessful);

            response.Headers.Should().NotBeEmpty();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/CatalogItems/acme-60458"));
        }
    }
}
