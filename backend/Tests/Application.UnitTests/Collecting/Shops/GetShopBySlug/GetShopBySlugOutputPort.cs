using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
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
