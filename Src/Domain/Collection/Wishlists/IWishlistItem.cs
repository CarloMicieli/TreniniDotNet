using NodaMoney;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistItem
    {
        WishlistItemId ItemId { get; }

        Priority Priority { get; }

        Money? Price { get; }

        ICatalogItem CatalogItem { get; }

        DeliveryDate? DeliveryDate { get; }

        string? Notes { get; }
    }
}
