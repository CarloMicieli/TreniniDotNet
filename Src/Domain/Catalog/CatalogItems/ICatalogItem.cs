using System.Collections.Generic;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Common.Entities;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItem : IModifiableEntity, ICatalogItemInfo
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