using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class CollectionItem : Entity<CollectionItemId>
    {
        private CollectionItem() { }

        public CollectionItem(
            CollectionItemId itemId,
            CatalogItem catalogItem,
            Condition condition,
            Price price,
            Shop? purchasedAt,
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

        public CatalogItem CatalogItem { get; } = null!;

        public Condition Condition { get; }

        public Price Price { get; } = null!;

        public Shop? PurchasedAt { get; }

        public LocalDate AddedDate { get; }

        public LocalDate? RemovedDate { get; }

        public string? Notes { get; }

        public CollectionItem With(
            CatalogItem? catalogItem = null,
            Condition? condition = null,
            Price? price = null,
            Shop? purchasedAt = null,
            LocalDate? addedDate = null,
            LocalDate? removedDate = null,
            string? notes = null) => new CollectionItem(Id, catalogItem ?? CatalogItem, condition ?? Condition,
            price ?? Price, purchasedAt ?? PurchasedAt, addedDate ?? AddedDate, removedDate ?? removedDate,
            notes ?? Notes);
    }
}
