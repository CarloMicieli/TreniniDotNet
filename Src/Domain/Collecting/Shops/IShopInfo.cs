using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public interface IShopInfo
    {
        ShopId Id { get; }

        Slug Slug { get; }

        string Name { get; }
    }
}
