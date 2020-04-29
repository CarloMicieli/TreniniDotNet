using System.Collections.Immutable;
using NodaMoney;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Collections
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
