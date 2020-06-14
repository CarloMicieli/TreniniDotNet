using NodaMoney;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public interface ICollectionItem
    {
        CollectionItemId Id { get; }

        ICatalogRef CatalogItem { get; }

        ICatalogItemDetails? Details { get; }

        Condition Condition { get; }

        Money Price { get; }

        IShopInfo? PurchasedAt { get; }

        LocalDate AddedDate { get; }

        string? Notes { get; }
    }
}
