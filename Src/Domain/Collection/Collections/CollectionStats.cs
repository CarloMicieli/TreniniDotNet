using NodaMoney;
using NodaTime;
using System.Collections.Immutable;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public sealed class CollectionStats : ICollectionStats
    {
        public CollectionStats(
            Owner owner,
            Instant modifiedDate,
            Money totalValue,
            IImmutableList<ICollectionStatsItem> categories)
        {
            Owner = owner;
            ModifiedDate = modifiedDate;
            TotalValue = totalValue;
            CategoriesByYear = categories;
        }

        public Owner Owner { get; }

        public Instant ModifiedDate { get; }

        public Money TotalValue { get; }

        public IImmutableList<ICollectionStatsItem> CategoriesByYear { get; }
    }
}
