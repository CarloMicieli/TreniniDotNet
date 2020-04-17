using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public interface ICollectionStatsItem
    {
        Category Category { get; }

        int Count { get; }

        Year Year { get; }
    }
}
