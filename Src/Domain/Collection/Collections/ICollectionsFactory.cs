using NodaMoney;
using NodaTime;
using System;
using System.Collections.Generic;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public interface ICollectionsFactory
    {
        ICollection NewCollection(string owner);

        ICollection NewCollection(
            Guid collectionId,
            string owner,
            IEnumerable<ICollectionItem> items,
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
