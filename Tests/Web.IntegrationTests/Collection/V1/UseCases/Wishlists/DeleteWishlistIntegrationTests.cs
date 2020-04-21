using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.Web.IntegrationTests.Collection.V1.UseCases.Wishlists
{
    public class DeleteWishlistIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/wishlists";

        public DeleteWishlistIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}