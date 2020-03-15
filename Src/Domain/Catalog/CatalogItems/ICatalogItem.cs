using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItem
    {
        CatalogItemId CatalogItemId { get; }
        IBrandInfo Brand { get; }
        Slug Slug { get; }
        ItemNumber ItemNumber { get; }
        IReadOnlyList<IRollingStock> RollingStocks { get; }
        string Description { get; }
        string? PrototypeDescription { get; }
        string? ModelDescription { get; }
        IScaleInfo Scale { get; }
        PowerMethod PowerMethod { get; }
    }
}