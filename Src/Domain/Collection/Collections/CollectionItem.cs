using NodaMoney;
using NodaTime;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public sealed class CollectionItem : ICollectionItem
    {
        internal CollectionItem(
            CollectionItemId itemId,
            ICatalogRef catalogItem,
            ICatalogItemDetails? details,
            Condition condition,
            Money price,
            IShopInfo? purchasedAt,
            LocalDate addedDate,
            string? notes)
        {
            ItemId = itemId;
            CatalogItem = catalogItem;
            Details = details;
            Condition = condition;
            Price = price;
            PurchasedAt = purchasedAt;
            AddedDate = addedDate;
            Notes = notes;
        }

        public CollectionItemId ItemId { get; }

        public ICatalogRef CatalogItem { get; }

        public ICatalogItemDetails? Details { get; }

        public Condition Condition { get; }

        public Money Price { get; }

        public IShopInfo? PurchasedAt { get; }

        public LocalDate AddedDate { get; }

        public string? Notes { get; }
    }
}
