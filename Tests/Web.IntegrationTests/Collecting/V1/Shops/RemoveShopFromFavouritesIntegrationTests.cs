using TreniniDotNet.Web;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Shops
{
    public class RemoveShopFromFavouritesIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/shops/favourites";

        public RemoveShopFromFavouritesIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}