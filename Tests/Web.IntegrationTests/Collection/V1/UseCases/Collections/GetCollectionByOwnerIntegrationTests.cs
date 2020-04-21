using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collection.V1.UseCases.Collections
{
    public class GetCollectionByOwnerIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/collections";

        public GetCollectionByOwnerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}