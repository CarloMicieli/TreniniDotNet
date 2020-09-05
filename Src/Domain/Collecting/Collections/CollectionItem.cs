using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class CollectionItem : Entity<CollectionItemId>
    {
        public CollectionItem(
            CollectionItemId itemId,
            CatalogItemRef catalogItem,
            Condition condition,
            Price price,
            ShopRef? purchasedAt,
            LocalDate addedDate,
            LocalDate? removedDate,
            string? notes)
        {
            Id = itemId;
            CatalogItem = catalogItem;
            Condition = condition;
            Price = price;
            PurchasedAt = purchasedAt;
            AddedDate = addedDate;
            RemovedDate = removedDate;
            Notes = notes;
        }

        public CatalogItemRef CatalogItem { get; }

        public Condition Condition { get; }

        public Price Price { get; }

        public ShopRef? PurchasedAt { get; }

        public LocalDate AddedDate { get; }

        public LocalDate? RemovedDate { get; }

        public string? Notes { get; }

        public CollectionItem With(
            CatalogItemRef? catalogItem = null,
            Condition? condition = null,
            Price? price = null,
            ShopRef? purchasedAt = null,
            LocalDate? addedDate = null,
            LocalDate? removedDate = null,
            string? notes = null)
        {
            return new CollectionItem(Id,
                catalogItem ?? CatalogItem,
                condition ?? Condition,
                price ?? Price,
                purchasedAt ?? PurchasedAt,
                addedDate ?? AddedDate,
                removedDate ?? removedDate,
                notes ?? Notes);
        }
    }
}
