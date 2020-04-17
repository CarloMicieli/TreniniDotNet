using System;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public sealed class CatalogItemDetails : ICatalogItemDetails
    {
        public CatalogItemDetails(
            IBrandRef brand, ItemNumber itemNumber,
            CollectionCategory category,
            IScaleRef scale,
            int rollingStocksCount,
            string description)
        {
            Brand = brand;
            ItemNumber = itemNumber;
            Category = category;
            Scale = scale;
            RollingStocksCount = rollingStocksCount;
            Description = description;
        }

        public static ICatalogItemDetails FromCatalogItem(ICatalogItem item) =>
            new CatalogItemDetails(
                BrandRef.FromBrandInfo(item.Brand),
                item.ItemNumber,
                CollectionCategories.FromCatalogItem(item),
                ScaleRef.FromScaleInfo(item.Scale),
                item.RollingStocks.Count,
                item.Description);

        public IBrandRef Brand { get; }

        public ItemNumber ItemNumber { get; }

        public CollectionCategory Category { get; }

        public IScaleRef Scale { get; }

        public int RollingStocksCount { get; }

        public string Description { get; }
    }
}
