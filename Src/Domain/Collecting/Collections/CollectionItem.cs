using NodaMoney;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class CollectionItem : Entity<CollectionItemId>, ICollectionItem
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
            : base(itemId)
        {
            CatalogItem = catalogItem;
            Details = details;
            Condition = condition;
            Price = price;
            PurchasedAt = purchasedAt;
            AddedDate = addedDate;
            Notes = notes;
        }

        public ICatalogRef CatalogItem { get; }

        public ICatalogItemDetails? Details { get; }

        public Condition Condition { get; }

        public Money Price { get; }

        public IShopInfo? PurchasedAt { get; }

        public LocalDate AddedDate { get; }

        public string? Notes { get; }
    }
}
