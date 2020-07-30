using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Collecting.V1.Collections.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Collections
{
    public class GetCollectionByOwnerIntegrationTests : AbstractWebApplicationFixture
    {
        public GetCollectionByOwnerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();

            var response = await client.GetAsync($"api/v1/collections/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldReturn404NotFound_WhenUserHasNoCollection()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = Guid.NewGuid();

            var response = await client.GetAsync($"api/v1/collections/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldReturnTheCollection()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var id = CollectingSeedData.Collections.GeorgeCollection().Id;

            var collection = await client.GetJsonAsync<CollectionResponse>($"api/v1/collections/{id}");

            collection.Should().NotBeNull();
            collection.Owner.Should().Be("George");
            collection.Id.Should().NotBeEmpty();
            collection.Items.Should().HaveCount(1);
        }
    }
}
