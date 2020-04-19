using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public interface IShopInfo
    {
        ShopId ShopId { get; }

        Slug Slug { get; }

        string Name { get; }
    }
}
