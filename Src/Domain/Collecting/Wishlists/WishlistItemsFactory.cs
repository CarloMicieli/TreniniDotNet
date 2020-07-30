using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public sealed class WishlistItemsFactory : EntityFactory<WishlistItemId, WishlistItem>
    {
        public WishlistItemsFactory(IGuidSource guidSource)
            : base(guidSource)
        {
        }

        public WishlistItem CreateWishlistItem(
            CatalogItem catalogItem,
            Priority priority,
            LocalDate addedDate,
            Price? price,
            string? notes)
        {
            return new WishlistItem(
                NewId(id => new WishlistItemId(id)),
                catalogItem,
                priority,
                addedDate,
                null,
                price,
                notes);
        }
    }
}
