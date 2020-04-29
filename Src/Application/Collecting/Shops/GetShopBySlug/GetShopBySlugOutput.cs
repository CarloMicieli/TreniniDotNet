using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
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
