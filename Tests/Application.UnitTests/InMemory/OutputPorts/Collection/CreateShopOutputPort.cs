using TreniniDotNet.Application.Boundaries.Collection.CreateShop;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
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
