using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collection.V1.UseCases.Collections
{
    public class AddItemToCollectionIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/collections/items";

        public AddItemToCollectionIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}