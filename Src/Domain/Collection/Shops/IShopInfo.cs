using TreniniDotNet.Common;
using TreniniDotNet.Common.Entities;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public interface IShopInfo : IModifiableEntity
    {
        ShopId ShopId { get; }

        Slug Slug { get; }

        string Name { get; }
    }
}
