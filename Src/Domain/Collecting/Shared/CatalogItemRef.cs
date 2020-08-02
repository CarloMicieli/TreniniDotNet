using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    //TODO: testme
    public sealed class CatalogItemRef : AggregateRootRef<CatalogItem, CatalogItemId>
    {
        public CatalogItemRef(CatalogItem catalogItem)
            : base(catalogItem.Id, catalogItem.Slug, catalogItem.ToString())
        {
            Count = catalogItem.Count;
            Category = catalogItem.Category;
            BrandName = catalogItem.Brand.ToString();
            ItemNumber = catalogItem.ItemNumber;
        }

        public CatalogItemRef(CatalogItemId id, string slug, 
            string brand,
            string itemNumber, 
            string description,
            IEnumerable<Category> rsCategories)
            : base(id, slug, description)
        {
            BrandName = brand;
            ItemNumber = new ItemNumber(itemNumber);
            Count = rsCategories.Count();
            Category = CatalogItemCategories.FromCategories(rsCategories);
        }
        
        public int Count { get; }
        public CatalogItemCategory Category { get; }
        public ItemNumber ItemNumber { get; }
        public string BrandName { get; }
    }
}