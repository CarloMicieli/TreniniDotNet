using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.Common;
using TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems
{
    public class PostCatalogItemsIntegrationTests : AbstractWebApplicationFixture
    {
        public PostCatalogItemsIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task PostCatalogItems_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var response = await client.PostJsonAsync("/api/v1/catalogItems", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PostCatalogItems_ShouldReturn400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var response = await client.PostJsonAsync("/api/v1/catalogItems", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PostCatalogItems_ShouldReturn422UnprocessableEntity_WhenScaleNotExist()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var content = new
            {
                brand = "Acme",
                itemNumber = "123456",
                description = "My new catalog item",
                powerMethod = "dc",
                scale = "not found",
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
        public async Task PostCatalogItems_ShouldReturn422UnprocessableEntity_WhenBrandNotExist()
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
        public async Task PostCatalogItems_ShouldReturn422UnprocessableEntity_WhenRailwayNotExist()
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
                    railway = "not found"
                })
            };

            var response = await client.PostJsonAsync("/api/v1/catalogItems", content, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task PostCatalogItems_ShouldReturn201Created_WhenNewLocomotiveIsCreated()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var model = new
            {
                brand = "Acme",
                itemNumber = "60530",
                description = @"Locomotiva elettrica 484 014 delle SBB, in livrea rosso-blu con
                         scritte sulle fiancate CHEMOIL. La Locomotiva presta servizio regolarmente in Italia.",
                prototypeDescription = "prototype description",
                modelDescription = "model description",
                powerMethod = "dc",
                scale = "H0",
                deliveryDate = "2020/Q2",
                available = true,
                rollingStocks = JsonArray(new
                {
                    lengthOverBuffer = new
                    {
                        millimeters = 217
                    },
                    epoch = "VI",
                    category = "ElectricLocomotive",
                    railway = "sbb",
                    className = "E 484",
                    roadNumber = "E 484 014",
                    dccInterface = "Mtc21",
                    control = "DccReady"
                })
            };

            var response = await client.PostJsonAsync("/api/v1/catalogItems", model, Check.IsSuccessful);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Should().NotBeEmpty();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/CatalogItems/acme-60530"));

            var content = await response.ExtractContent<PostCatalogItemResponse>();
            content.Slug.Value.Should().Be(Slug.Of("acme-60530"));
        }

        [Fact]
        public async Task PostCatalogItems_ShouldReturn201Created_WhenNewPassengerCarIsCreated()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            var model = new
            {
                brand = "Roco",
                itemNumber = "74601",
                description = @"Carrozza mista di 1/2° KL. tipo 1921, nella livrea grigio/ardesia delle FS ep. IV",
                prototypeDescription = "prototype description",
                modelDescription = "model description",
                powerMethod = "dc",
                scale = "H0",
                deliveryDate = "2020/Q2",
                available = true,
                rollingStocks = JsonArray(new
                {
                    lengthOverBuffer = new
                    {
                        millimeters = 242
                    },
                    epoch = "IV",
                    category = "PassengerCar",
                    railway = "fs",
                    typeName = "Tipo 1921",
                    serviceLevel = "1cl/2cl",
                    passengerCarType = "Coach"
                })
            };

            var response = await client.PostJsonAsync("/api/v1/catalogItems", model, Check.IsSuccessful);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Should().NotBeEmpty();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/CatalogItems/roco-74601"));

            var content = await response.ExtractContent<PostCatalogItemResponse>();
            content.Slug.Value.Should().Be(Slug.Of("roco-74601"));
        }
    }
}
