using System.Collections.Immutable;
using NodaMoney;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public interface ICollectionStats
    {
        CollectionId Id { get; }

        Owner Owner { get; }

        Instant ModifiedDate { get; }

        Money TotalValue { get; }

        IImmutableList<ICollectionStatsItem> CategoriesByYear { get; }
    }
}
