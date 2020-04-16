using NodaMoney;
using NodaTime;
using TreniniDotNet.Domain.Collection.Shared;
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

        IShopInfo? PurchasedAt { get; }

        LocalDate AddedDate { get; }

        string? Notes { get; }
    }
}
