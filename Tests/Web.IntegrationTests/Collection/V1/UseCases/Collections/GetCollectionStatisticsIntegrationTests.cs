using FluentAssertions;
using IntegrationTests;
using System;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Collection.V1.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collection.V1.UseCases.Collections
{
    public class GetCollectionStatisticsIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/collections/statistics";

        public GetCollectionStatisticsIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();

            var response = await client.GetAsync($"api/v1/collections/{id}/statistics");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldReturn404NotFound_WhenTheCollectionIsNotFound()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = Guid.NewGuid();

            var response = await client.GetAsync($"api/v1/collections/{id}/statistics");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldReturn404NotFound_WhenUserIsNotTheCollectionOwner()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = CollectionSeedData.Collections.GeorgeCollection().CollectionId;

            var response = await client.GetAsync($"api/v1/collections/{id}/statistics");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldReturnTheCollectionStatistics()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var id = CollectionSeedData.Collections.GeorgeCollection().CollectionId;

            var statistics = await client.GetJsonAsync<CollectionStatisticsResponse>($"api/v1/collections/{id}/statistics");

            statistics.Should().NotBeNull();
            statistics.Id.Should().Be(id.ToGuid());
            statistics.Owner.Should().Be("George");
        }
    }
}