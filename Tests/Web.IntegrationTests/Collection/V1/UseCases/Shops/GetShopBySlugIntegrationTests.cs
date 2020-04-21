using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.Web.IntegrationTests.Collection.V1.UseCases.Shops
{
    public class GetShopBySlugIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/shops";

        public GetShopBySlugIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}