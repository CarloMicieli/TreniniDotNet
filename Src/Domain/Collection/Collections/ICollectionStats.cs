using NodaMoney;
using NodaTime;
using System.Collections.Immutable;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public interface ICollectionStats
    {
        Owner Owner { get; }

        Instant ModifiedDate { get; }

        Money TotalValue { get; }

        IImmutableList<ICollectionStatsItem> CategoriesByYear { get; }
    }
}
