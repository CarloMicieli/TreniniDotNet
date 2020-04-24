using NodaMoney;
using NodaTime;
using System.Collections.Immutable;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public sealed class CollectionStats : ICollectionStats
    {
        public CollectionStats(
            CollectionId id,
            Owner owner,
            Instant modifiedDate,
            Money totalValue,
            IImmutableList<ICollectionStatsItem> categories)
        {
            Id = id;
            Owner = owner;
            ModifiedDate = modifiedDate;
            TotalValue = totalValue;
            CategoriesByYear = categories;
        }

        public CollectionId Id { get; }

        public Owner Owner { get; }

        public Instant ModifiedDate { get; }

        public Money TotalValue { get; }

        public IImmutableList<ICollectionStatsItem> CategoriesByYear { get; }
    }
}
