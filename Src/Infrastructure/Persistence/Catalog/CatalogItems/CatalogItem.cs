using System;
using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Infrastracture.Persistence.Catalog.CatalogItems;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Scales;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public class CatalogItem 
    {
        public CatalogItemId CatalogItemId { set; get; }

        public Brand Brand { set; get; } = null!;

        public string Slug { set; get; }

        public string Category { set; get; } = "";

        public Scale Scale { set; get; } = null!;

        public string ItemNumber { set; get; } = null!;

        public List<RollingStock> RollingStocks { set; get; } = null!;

        public string Description { set; get; } = "";

        public string? PrototypeDescription { set; get; }

        public string? ModelDescription { set; get; }

        public string? DeliveryDate { set; get; }

        public bool? DirectCurrent { set; get; }

        public bool? Released { set; get; }

        public DateTime? CreatedAt { set; get; }
    }
}