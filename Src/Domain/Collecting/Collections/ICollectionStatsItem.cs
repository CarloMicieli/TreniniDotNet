using NodaMoney;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public interface ICollectionStatsItem
    {
        CollectionCategory Category { get; }

        int Count { get; }

        Year Year { get; }

        Money TotalValue { get; }
    }
}
