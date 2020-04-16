using NodaMoney;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public sealed class CollectionsFactory : ICollectionsFactory
    {
        private readonly IClock _clock;
        private readonly IGuidSource _guidSource;

        public CollectionsFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock ??
                throw new ArgumentNullException(nameof(clock));

            _guidSource = guidSource ??
                throw new ArgumentNullException(nameof(guidSource));
        }

        public ICollection NewCollection(string owner)
        {
            return new Collection(
                new CollectionId(_guidSource.NewGuid()),
                owner,
                ImmutableList<ICollectionItem>.Empty,
                _clock.GetCurrentInstant(),
                null,
                1);
        }

        public ICollection NewCollection(
            Guid collectionId,
            string owner,
            IEnumerable<ICollectionItem> items,
            DateTime createdDate,
            DateTime? modifiedDate,
            int version)
        {
            return new Collection(
                new CollectionId(collectionId),
                owner,
                items.ToImmutableList(),
                createdDate.ToUtc(),
                modifiedDate.ToUtcOrDefault(),
                version);
        }

        public ICollectionItem NewCollectionItem(
            ICatalogItem catalogItem,
            Condition condition,
            Money price,
            LocalDate added,
            IShopInfo? purchasedAt,
            string? notes)
        {
            return NewCollectionItem(
                new CollectionItemId(_guidSource.NewGuid()),
                catalogItem,
                condition,
                price,
                added,
                purchasedAt,
                notes);
        }

        public ICollectionItem NewCollectionItem(
            CollectionItemId itemId,
            ICatalogItem catalogItem,
            Condition condition,
            Money price,
            LocalDate added,
            IShopInfo? purchasedAt,
            string? notes)
        {
            return new CollectionItem(
                itemId,
                catalogItem,
                condition,
                price,
                purchasedAt,
                added,
                notes);
        }
    }
}
