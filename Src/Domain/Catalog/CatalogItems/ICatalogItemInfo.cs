using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItemInfo
    {
        CatalogItemId CatalogItemId { get; }
        IBrandInfo Brand { get; }
        Slug Slug { get; }
        ItemNumber ItemNumber { get; }
    }
}
