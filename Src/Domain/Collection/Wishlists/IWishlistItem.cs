using NodaMoney;
using NodaTime;
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

        Instant AddedDate { get; }

        string? Notes { get; }
    }
}
