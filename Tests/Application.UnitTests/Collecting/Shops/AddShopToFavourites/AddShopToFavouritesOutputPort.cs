using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites
{
    public sealed class AddShopToFavouritesOutputPort : OutputPortTestHelper<AddShopToFavouritesOutput>, IAddShopToFavouritesOutputPort
    {
        private MethodInvocation<ShopId> ShopNotFoundMethod { get; set; }

        public AddShopToFavouritesOutputPort()
        {
            ShopNotFoundMethod = MethodInvocation<ShopId>.NotInvoked(nameof(ShopNotFound));
        }

        public void ShopNotFound(ShopId shopId)
        {
            ShopNotFoundMethod = ShopNotFoundMethod.Invoked(shopId);
        }

        public void AssertShopNotFound(ShopId expectedShopId)
        {
            ShopNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedShopId);
        }
    }
}
