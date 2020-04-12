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
            var content = new
            {
                Name = "New Brand",
                CompanyName = "Associazione Costruzioni Modellistiche Esatte",
                WebsiteUrl = "http://www.acmetreni.com",
                EmailAddress = "mail@acmetreni.com",
                BrandType = "Industrial"
            };

            var response = await client.PostJsonAsync("/api/v1/brands", content, Check.IsSuccessful);

            response.EnsureSuccessStatusCode();

            response.Headers.Should().NotBeEmpty();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/Brands/new-brand"));
        }

        [Fact]
        public async Task CreateNewBrands_ReturnsError_WhenTheRequestIsInvalid()
        {
            // Arrange
            var client = CreateHttpClient();
            var content = JsonContent(new
            {
                CompanyName = "Associazione Costruzioni Modellistiche Esatte",
                WebsiteUrl = "http://www.acmetreni.com",
                EmailAddress = "mail@acmetreni.com",
                BrandType = "Industrial"
            });

            // Act
            var response = await client.PostAsync("/api/v1/brands", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateNewBrands_ReturnsBadRequest_WhenTheBrandAlreadyExist()
        {
            // Arrange
            var client = CreateHttpClient();
            var content = JsonContent(new
            {
                Name = "ACME",
                CompanyName = "Associazione Costruzioni Modellistiche Esatte",
                WebsiteUrl = "http://www.acmetreni.com",
                EmailAddress = "mail@acmetreni.com",
                BrandType = "Industrial"
            });

            // Act
            var response = await client.PostAsync("/api/v1/brands", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

