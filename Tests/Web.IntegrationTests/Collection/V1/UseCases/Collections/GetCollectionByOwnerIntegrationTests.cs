using FluentAssertions;
using IntegrationTests;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Collection.V1.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collection.V1.UseCases.Collections
{
    public class GetCollectionByOwnerIntegrationTests : AbstractWebApplicationFixture
    {
        private const string CollectionsUri = "api/v1/collections";

        public GetCollectionByOwnerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync(CollectionsUri);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldReturn404NotFound_WhenUserHasNoCollection()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var response = await client.GetAsync(CollectionsUri);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldReturnTheCollection()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var collection = await client.GetJsonAsync<CollectionResponse>(CollectionsUri);

            collection.Should().NotBeNull();
            collection.Owner.Should().Be("George");
            collection.Id.Should().NotBeEmpty();
            collection.Items.Should().HaveCount(1);
        }
    }
}