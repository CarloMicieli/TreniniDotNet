using System;
using System.Collections.Immutable;
using NodaMoney;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public interface ICollectionsFactory
    {
        ICollection NewCollection(string owner);

        ICollection NewCollection(
            Guid collectionId,
            string owner,
            IImmutableList<ICollectionItem> items,
            DateTime createdDate,
            DateTime? modifiedDate,
            int version);

        ICollectionItem NewCollectionItem(
            ICatalogRef catalogItem,
            ICatalogItemDetails? details,
            Condition condition,
            Money price,
            LocalDate added,
            IShopInfo? shop,
            string? notes);

        ICollectionItem NewCollectionItem(
            CollectionItemId id,
            ICatalogRef catalogItem,
            ICatalogItemDetails? details,
            Condition condition,
            Money price,
            LocalDate added,
            IShopInfo? shop,
            string? notes);
    }
}
