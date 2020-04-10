using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItem : ICatalogItemInfo
    {
        IReadOnlyList<IRollingStock> RollingStocks { get; }
        string Description { get; }
        string? PrototypeDescription { get; }
        string? ModelDescription { get; }
        DeliveryDate? DeliveryDate { get; }
        bool IsAvailable { get; }
        IScaleInfo Scale { get; }
        PowerMethod PowerMethod { get; }

        ICatalogItemInfo ToCatalogItemInfo();
    }
}