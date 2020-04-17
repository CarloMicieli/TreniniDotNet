using NodaMoney;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistItem
    {
        WishlistItemId ItemId { get; }

        Priority Priority { get; }

        Money? Price { get; }

        ICatalogRef CatalogItem { get; }

        ICatalogItemDetails? Details { get; }

        DeliveryDate? DeliveryDate { get; }

        string? Notes { get; }
    }
}
