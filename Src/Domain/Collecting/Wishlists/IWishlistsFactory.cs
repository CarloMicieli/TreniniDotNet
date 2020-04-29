using System.Collections.Immutable;
using NodaMoney;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public interface IWishlistsFactory
    {
        IWishlist NewWishlist(Owner owner, Slug slug, string? listName, Visibility visibility);

        IWishlist NewWishlist(
            WishlistId wishlistId,
            Owner owner,
            Slug slug, string? listName,
            Visibility visibility,
            IImmutableList<IWishlistItem> items,
            Instant createdDate,
            Instant? modifiedDate,
            int version);

        IWishlistItem NewWishlistItem(
            ICatalogRef catalogItem,
            Priority priority,
            LocalDate AddedDate,
            Money? price,
            string? notes);

        IWishlistItem NewWishlistItem(
            WishlistItemId itemId,
            ICatalogRef catalogItem,
            ICatalogItemDetails? details,
            Priority priority,
            LocalDate addedDate,
            Money? price,
            string? notes);
    }
}
