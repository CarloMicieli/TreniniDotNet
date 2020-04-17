using NodaMoney;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public sealed class CollectionStatsItem : ICollectionStatsItem
    {
        public CollectionStatsItem(Year year, CollectionCategory category, int count, Money totalValue)
        {
            Category = category;
            Count = count;
            Year = year;
            TotalValue = totalValue;
        }

        public CollectionCategory Category { get; }

        public Money TotalValue { get; }

        public int Count { get; }

        public Year Year { get; }
    }
}
