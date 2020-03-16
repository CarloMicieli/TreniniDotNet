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
        public async Task CreateNewRailways_ReturnsOk()
        {
            var client = CreateHttpClient();
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
