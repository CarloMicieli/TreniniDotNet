using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Shops.CreateShop
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

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    ShopAlreadyExistsMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
