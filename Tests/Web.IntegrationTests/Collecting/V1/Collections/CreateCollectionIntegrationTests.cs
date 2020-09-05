using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Collections
{
    public class CreateCollectionIntegrationTests : AbstractWebApplicationFixture
    {
        public CreateCollectionIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task CreateCollection_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var response = await client.PostJsonAsync("/api/v1/collections", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task CreateCollection_ShouldReturn409Conflict_WhenUserHasAlreadyCollection()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var response = await client.PostJsonAsync("/api/v1/collections", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task CreateCollection_ShouldCreateNewCollections()
        {
            var client = CreateHttpClient("Ciccins", "Pa$$word88");

            var request = new
            {
                Notes = "My first wonderful collection"
            };

            var response = await client.PostJsonAsync("/api/v1/collections", request, Check.IsSuccessful);

            var content = await response.ExtractContent<CollectionCreated>();
            content.Should().NotBeNull();
            content.Id.Should().NotBeEmpty();
            content.Owner.Should().Be("Ciccins");
        }
    }

    internal class CollectionCreated
    {
        public Guid Id { set; get; }
        public string Owner { set; get; }
    }
}
