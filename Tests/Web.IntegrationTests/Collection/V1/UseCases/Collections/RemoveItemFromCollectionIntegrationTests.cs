using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collection.V1.UseCases.Collections
{
    public class RemoveItemFromCollectionIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/collections/items";

        public RemoveItemFromCollectionIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}