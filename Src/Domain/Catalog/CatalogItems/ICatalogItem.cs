using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItem
    {
        CatalogItemId CatalogItemId { get; }
        Brand Brand { get; }
        Slug Slug { get; }
        ItemNumber ItemNumber { get; }
        IEnumerable<RollingStock> RollingStocks { get; }
        string Description { get; }
        string? PrototypeDescription { get; }
        string? ModelDescription { get; }
    }
}