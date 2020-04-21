using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.Web.IntegrationTests.Collection.V1.UseCases.Wishlists
{
    public class AddItemToWishlistIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/wishlists";

        public AddItemToWishlistIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}