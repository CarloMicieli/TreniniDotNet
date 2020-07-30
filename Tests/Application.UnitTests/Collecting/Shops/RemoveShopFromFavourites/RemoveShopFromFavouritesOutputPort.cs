using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites
{
    public sealed class RemoveShopFromFavouritesOutputPort : OutputPortTestHelper<RemoveShopFromFavouritesOutput>, IRemoveShopFromFavouritesOutputPort
    {
        private MethodInvocation<ShopId> ShopNotFoundMethod { get; set; }

        public RemoveShopFromFavouritesOutputPort()
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
