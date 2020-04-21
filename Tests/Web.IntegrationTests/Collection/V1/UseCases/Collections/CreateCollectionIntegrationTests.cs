using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collection.V1.UseCases.Collections
{
    public class CreateCollectionIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/Collections";

        public CreateCollectionIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}