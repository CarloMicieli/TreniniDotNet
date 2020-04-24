using TreniniDotNet.Domain.Collection.Shops;

namespace TreniniDotNet.Application.Boundaries.Collection.GetShopBySlug
{
    public sealed class GetShopBySlugOutput : IUseCaseOutput
    {
        public GetShopBySlugOutput(IShop shop)
        {
            Shop = shop;
        }

        public IShop Shop { get; }
    }
}
