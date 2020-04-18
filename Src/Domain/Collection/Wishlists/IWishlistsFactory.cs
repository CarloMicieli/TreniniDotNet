using NodaMoney;
using NodaTime;
using System.Collections.Immutable;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistsFactory
    {
        IWishList NewWishlist(Owner owner, Slug slug, string? listName, Visibility visibility);

        IWishList NewWishlist(
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
