using NodaMoney;
using NodaTime;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistItem
    {
        WishlistItemId ItemId { get; }

        Priority Priority { get; }

        LocalDate AddedDate { get; }

        Money? Price { get; }

        ICatalogRef CatalogItem { get; }

        ICatalogItemDetails? Details { get; }

        string? Notes { get; }
    }
}
