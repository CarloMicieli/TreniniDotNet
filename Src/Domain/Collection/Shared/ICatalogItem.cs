using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public interface ICatalogItem
    {
        Slug Slug { set; }

        string Brand { get; }

        ItemNumber ItemNumber { get; }

        Category Category { get; }
    }
}
