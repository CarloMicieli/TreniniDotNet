using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateShop
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
