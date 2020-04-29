using TreniniDotNet.Application.Collecting.Shops.CreateShop;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Collecting.Shops.OutputPorts
{
    public sealed class CreateShopOutputPort : OutputPortTestHelper<CreateShopOutput>, ICreateShopOutputPort
    {
        private MethodInvocation<string> ShopAlreadyExistsMethod { set; get; }

        public CreateShopOutputPort()
        {
            ShopAlreadyExistsMethod = MethodInvocation<string>.NotInvoked(nameof(ShopAlreadyExists));
        }

        public void ShopAlreadyExists(string shopName)
        {
            ShopAlreadyExistsMethod = ShopAlreadyExistsMethod.Invoked(shopName);
        }

        public void AssertShopAlreadyExists(string expectedShopName) =>
            ShopAlreadyExistsMethod.ShouldBeInvokedWithTheArgument(expectedShopName);
    }
}
