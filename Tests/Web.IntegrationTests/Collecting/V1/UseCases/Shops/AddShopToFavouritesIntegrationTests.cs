using IntegrationTests;
using TreniniDotNet.Web;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.UseCases.Shops
{
    public class AddShopToFavouritesIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/shops/favourites";

        public AddShopToFavouritesIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}