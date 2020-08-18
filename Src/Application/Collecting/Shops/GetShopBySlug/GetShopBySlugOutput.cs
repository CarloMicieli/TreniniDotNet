using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
{
    public sealed class GetShopBySlugOutput : IUseCaseOutput
    {
        public GetShopBySlugOutput(Shop shop)
        {
            Shop = shop;
        }

        public Shop Shop { get; }
    }
}
