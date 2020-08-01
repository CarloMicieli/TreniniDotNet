using TreniniDotNet.Web;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Shops
{
    public class GetFavouriteShopsIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/shops/favourites";

        public GetFavouriteShopsIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}