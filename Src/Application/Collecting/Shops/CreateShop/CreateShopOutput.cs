using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

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
