using NodaMoney;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public interface ICollectionStatsItem
    {
        CollectionCategory Category { get; }

        int Count { get; }

        Year Year { get; }

        Money TotalValue { get; }
    }
}
