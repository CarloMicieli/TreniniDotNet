using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class CollectionStatsItem
    {
        public CollectionStatsItem(Year year, CatalogItemCategory category, int count, Price totalValue)
        {
            Category = category;
            Count = count;
            Year = year;
            TotalValue = totalValue;
        }

        public CatalogItemCategory Category { get; }

        public Price TotalValue { get; }

        public int Count { get; }

        public Year Year { get; }
    }
}
