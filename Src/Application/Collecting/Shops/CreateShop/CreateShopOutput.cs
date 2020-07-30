using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Shops.CreateShop
{
    public sealed class CreateShopOutput : IUseCaseOutput
    {
        public CreateShopOutput(ShopId id, Slug slug)
        {
            Id = id;
            Slug = slug;
        }

        public ShopId Id { get; }
        public Slug Slug { get; }
    }
}
