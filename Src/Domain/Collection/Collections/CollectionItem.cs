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
            ICatalogItem catalogItem,
            Condition condition,
            Money price,
            IShopInfo? purchasedAt,
            LocalDate addedDate,
            string? notes)
        {
            ItemId = itemId;
            CatalogItem = catalogItem;
            Condition = condition;
            Price = price;
            PurchasedAt = purchasedAt;
            AddedDate = addedDate;
            Notes = notes;
        }

        public CollectionItemId ItemId { get; }

        public ICatalogItem CatalogItem { get; }

        public Condition Condition { get; }

        public Money Price { get; }

        public IShopInfo? PurchasedAt { get; }

        public LocalDate AddedDate { get; }

        public string? Notes { get; }
    }
}
