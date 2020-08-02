using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    public sealed class CatalogItemRef : AggregateRootRef<CatalogItem, CatalogItemId>
    {
        public CatalogItemRef(CatalogItem catalogItem)
            : base(catalogItem.Id, catalogItem.Slug, catalogItem.ToString())
        {
            Count = catalogItem.Count;
            Category = catalogItem.Category;
        }

        public static CatalogItemRef? AsOptional(CatalogItem? catalogItem) =>
            (catalogItem is null) ? null : new CatalogItemRef(catalogItem);
        
        public int Count { get; }
        public CatalogItemCategory Category { get; }
    }
}