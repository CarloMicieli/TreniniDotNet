﻿using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public sealed class WishlistItem : Entity<WishlistItemId>
    {
        public WishlistItem(
            WishlistItemId itemId,
            CatalogItemRef catalogItem,
            Priority priority,
            LocalDate addedDate,
            LocalDate? removedDate,
            Price? price,
            string? notes)
        {
            Id = itemId;
            Priority = priority;
            AddedDate = addedDate;
            RemovedDate = removedDate;
            Price = price;
            CatalogItem = catalogItem;
            Notes = notes;
        }

        #region [ Properties ]
        public Priority Priority { get; }

        public LocalDate AddedDate { get; }

        public LocalDate? RemovedDate { get; }

        public Price? Price { get; }

        public CatalogItemRef CatalogItem { get; }

        public string? Notes { get; }
        #endregion

        public WishlistItem With(
            CatalogItemRef? catalogItem = null,
            Priority? priority = null,
            LocalDate? addedDate = null,
            LocalDate? removedDate = null,
            Price? price = null,
            string? notes = null)
        {
            return new WishlistItem(Id,
                catalogItem ?? CatalogItem,
                priority ?? Priority,
                addedDate ?? AddedDate,
                removedDate ?? RemovedDate,
                price ?? Price,
                notes ?? Notes);
        }

        public override string ToString() => $"WishlistItem({Id}, {CatalogItem}, {Priority})";
    }
}
