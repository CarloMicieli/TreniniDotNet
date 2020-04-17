using NodaMoney;
using NodaTime;
using System.Collections.Immutable;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public interface ICollectionStats
    {
        string Owner { get; }

        Instant ModifiedDate { get; }

        Money TotalValue { get; }

        IImmutableList<ICollectionStatsItem> Categories { get; }
    }
}
