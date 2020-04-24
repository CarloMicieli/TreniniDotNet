using NodaMoney;
using NodaTime;
using System.Collections.Immutable;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
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
