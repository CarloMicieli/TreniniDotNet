using TreniniDotNet.Application.Collecting.Shops.GetShopBySlug;
using TreniniDotNet.Common;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Collecting.Shops.OutputPorts
{
    public class GetShopBySlugOutputPort : OutputPortTestHelper<GetShopBySlugOutput>, IGetShopBySlugOutputPort
    {
        private MethodInvocation<Slug> ShopNotFoundMethod { set; get; }

        public GetShopBySlugOutputPort()
        {
            ShopNotFoundMethod = MethodInvocation<Slug>.NotInvoked(nameof(ShopNotFound));
        }

        public void ShopNotFound(Slug slug)
        {
            ShopNotFoundMethod = ShopNotFoundMethod.Invoked(slug);
        }

        public void AssertShopWasNotFound(Slug expectedSlug) =>
            ShopNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedSlug);
    }
}
