using NodaMoney;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public sealed class WishlistItem : Entity<WishlistItemId>, IWishlistItem
    {
        internal WishlistItem(
            WishlistItemId itemId,
            ICatalogRef catalogItem,
            ICatalogItemDetails? details,
            Priority priority,
            LocalDate addedDate,
            Money? price,
            string? notes)
            : base(itemId)
        {
            Priority = priority;
            AddedDate = addedDate;
            Price = price;
            CatalogItem = catalogItem;
            Details = details;
            Notes = notes;
        }

        public Priority Priority { get; }

        public LocalDate AddedDate { get; }

        public Money? Price { get; }

        public ICatalogRef CatalogItem { get; }

        public ICatalogItemDetails? Details { get; }

        public string? Notes { get; }

        public override string ToString() => $"WishlistItem({Id}, {CatalogItem}, {Priority})";
    }
}
