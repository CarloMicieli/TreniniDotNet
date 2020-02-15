using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public class CatalogItem : ICatalogItem
    {
        public CatalogItemId CatalogItemId { set; get; }

        public Brand Brand { set; get; } = null!;

        public Slug Slug { set; get; }

        public ItemNumber ItemNumber { set; get; }

        public IEnumerable<RollingStock> RollingStocks { set; get; } = new List<RollingStock>();

        public string Description { set; get; } = "";

        public string? PrototypeDescription { set; get; }

        public string? ModelDescription { set; get; }
    }
}