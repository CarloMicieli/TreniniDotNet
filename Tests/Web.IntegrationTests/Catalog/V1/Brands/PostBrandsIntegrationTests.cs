using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Catalog.V1.Brands.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Brands
{
    public class PostBrandsIntegrationTests : AbstractWebApplicationFixture
    {
        private readonly HttpClient client;
        
        public PostBrandsIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/v1/brands/");
            client = factory.CreateClient();
        }

        [Fact]
        public async Task PostBrands_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var response = await client.PostJsonAsync("/api/v1/brands", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PostBrands_ShouldReturn400BadRequest_WhenTheRequestIsInvalid()
        {
            var client = CreateAuthorizedHttpClient();
            
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
        public async Task PostBrands_ShouldReturn409Conflict_WhenTheBrandAlreadyExist()
        {
            var client = CreateAuthorizedHttpClient();

            var request = new
            {
                name = "ACME",
                companyName = "Associazione Costruzioni Modellistiche Esatte",
                websiteUrl = "http://www.acmetreni.com",
                emailAddress = "mail@acmetreni.com",
                brandType = "Industrial"
            };

            var response = await client.PostJsonAsync("/api/v1/brands", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task PostBrands_ShouldReturn201Created_WhenCreateTheNewBrand()
        {
            var client = CreateAuthorizedHttpClient();

            var request = new
            {
                name = "New Brand",
                companyName = "Company Name",
                groupName = "Group name",
                websiteUrl = "http://www.newbrand.com",
                emailAddress = "mail@newbrand.com",
                brandType = "Industrial",
                description = "Brand description",
                address = new
                {
                    line1 = "22 Acacia Avenue",
                    line2 = "Unit 1",
                    region = "West End",
                    postalCode = "123456",
                    city = "London",
                    country = "GB"
                }
            };

            var response = await client.PostJsonAsync("/api/v1/brands", request, Check.IsSuccessful);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Should().NotBeEmpty();
            response.Headers.Location.Should().Be(new Uri("http://localhost/api/v1/Brands/new-brand"));

            var content = await response.ExtractContent<PostBrandResponse>();
            content.Slug.Value.Should().Be("new-brand");
        }
    }
}
