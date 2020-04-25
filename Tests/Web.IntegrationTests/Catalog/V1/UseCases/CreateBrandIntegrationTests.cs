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
    public class CreateBrandIntegrationTests : AbstractWebApplicationFixture
    {
        public CreateBrandIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task CreateNewBrands_ReturnsOk()
        {
            var client = CreateHttpClient();

            var request = new
            {
                Name = "New Brand",
                CompanyName = "Associazione Costruzioni Modellistiche Esatte",
                WebsiteUrl = "http://www.acmetreni.com",
                EmailAddress = "mail@acmetreni.com",
                BrandType = "Industrial"
            };

            var response = await client.PostJsonAsync("/api/v1/brands", request, Check.IsSuccessful);

            response.Headers.Should().NotBeEmpty();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/Brands/new-brand"));
        }

        [Fact]
        public async Task CreateNewBrands_ReturnsError_WhenTheRequestIsInvalid()
        {
            var client = CreateHttpClient();

            var request = new
            {
                CompanyName = "Associazione Costruzioni Modellistiche Esatte",
                WebsiteUrl = "http://www.acmetreni.com",
                EmailAddress = "mail@acmetreni.com",
                BrandType = "Industrial"
            };

            var response = await client.PostJsonAsync("/api/v1/brands", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateNewBrands_ReturnsBadRequest_WhenTheBrandAlreadyExist()
        {
            var client = CreateHttpClient();

            var request = new
            {
                Name = "ACME",
                CompanyName = "Associazione Costruzioni Modellistiche Esatte",
                WebsiteUrl = "http://www.acmetreni.com",
                EmailAddress = "mail@acmetreni.com",
                BrandType = "Industrial"
            };

            var response = await client.PostJsonAsync("/api/v1/brands", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}

