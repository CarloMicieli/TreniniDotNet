using NodaMoney;
using NodaTime;
using System;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public sealed class WishlistItem : IWishlistItem, IEquatable<WishlistItem>
    {
        internal WishlistItem(
            WishlistItemId itemId,
            ICatalogRef catalogItem,
            ICatalogItemDetails? details,
            Priority priority,
            LocalDate addedDate,
            Money? price,
            string? notes)
        {
            ItemId = itemId;
            Priority = priority;
            AddedDate = addedDate;
            Price = price;
            CatalogItem = catalogItem;
            Details = details;
            Notes = notes;
        }

        public WishlistItemId ItemId { get; }

        public Priority Priority { get; }

        public LocalDate AddedDate { get; }

        public Money? Price { get; }

        public ICatalogRef CatalogItem { get; }

        public ICatalogItemDetails? Details { get; }

        public string? Notes { get; }

        #region [ Equality ]

        public override bool Equals(object obj)
        {
            if (obj is WishlistItem that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(WishlistItem other) => AreEquals(this, other);

        public static bool operator ==(WishlistItem left, WishlistItem right) => AreEquals(left, right);

        public static bool operator !=(WishlistItem left, WishlistItem right) => !AreEquals(left, right);

        private static bool AreEquals(WishlistItem left, WishlistItem right) =>
            left.ItemId == right.ItemId;

        #endregion

        public override int GetHashCode() => ItemId.GetHashCode();

        public override string ToString() => $"WishlistItem({ItemId}, {CatalogItem}, {Priority})";
    }
}
