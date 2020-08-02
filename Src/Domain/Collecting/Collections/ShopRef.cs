using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class ShopRef : AggregateRootRef<Shop, ShopId>
    {
        public ShopRef(ShopId id, string slug, string name) 
            : base(id, slug, name)
        {
        }

        public static ShopRef? AsOptional(Shop? shop) =>
            (shop is null) ? null : new ShopRef(shop.Id, shop.Slug.Value, shop.Name);
    }
}