using NodaMoney;
using NodaTime;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public interface ICollectionItem
    {
        CollectionItemId ItemId { get; }

        ICatalogItem CatalogItem { get; }

        Condition Condition { get; }

        Money Price { get; }

        IShop? PurchasedAt { get; }

        Instant AddedDate { get; }

        string? Notes { get; }
    }
}
