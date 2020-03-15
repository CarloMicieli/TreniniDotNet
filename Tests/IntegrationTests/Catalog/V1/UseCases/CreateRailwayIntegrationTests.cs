using IntegrationTests;
using System;
using System.Net;
using System.Threading.Tasks;
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
        public async Task CreateNewRailways_ReturnsOk()
        {
            // Arrange
            var client = CreateHttpClient();
            var content = JsonContent(new
            {
                Name = "New Railway",
                CompanyName = "Ferrovie dello stato",
                Country = "IT",
                Status = "Active",
                OperatingSince = new DateTime(1905, 7, 1)
            });

            // Act
            var response = await client.PostAsync("/api/v1/railways", content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.NotNull(response.Headers.Location);
            Assert.Equal(new Uri("http://localhost/api/v1/Railways/new-railway"), response.Headers.Location);
        }

        [Fact]
        public async Task CreateNewBrands_ReturnsError_WhenTheRequestIsInvalid()
        {
            // Arrange
            var client = CreateHttpClient();
            var content = JsonContent(new
            {
                CompanyName = "Ferrovie dello stato",
                Country = "IT",
                Status = "Active",
                OperatingSince = new DateTime(1905, 7, 1)
            });

            // Act
            var response = await client.PostAsync("/api/v1/railways", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateNewBrands_ReturnsBadRequest_WhenTheRailwayAlreadyExist()
        {
            // Arrange
            var client = CreateHttpClient();
            var content = JsonContent(new
            {
                Name = "FS",
                CompanyName = "Ferrovie dello stato",
                Country = "IT",
                Status = "Active",
                OperatingSince = new DateTime(1905, 7, 1)
            });

            // Act
            var response = await client.PostAsync("/api/v1/railways", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
