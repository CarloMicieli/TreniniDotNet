using System;
using System.Collections.Generic;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Scales;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public class CatalogItem
    {
        public Guid CatalogItemId { set; get; }

        public Brand Brand { set; get; } = null!;

        public string Slug { set; get; } = null!;

        public Scale Scale { set; get; } = null!;

        public string ItemNumber { set; get; } = null!;

        public List<RollingStock> RollingStocks { set; get; } = new List<RollingStock>();

        public string Description { set; get; } = "";

        public string? PrototypeDescription { set; get; }

        public string? ModelDescription { set; get; }

        public string? DeliveryDate { set; get; }

        public string? PowerMethod { set; get; }

        public bool? Released { set; get; }

        public DateTime? CreatedAt { set; get; }
    }
}