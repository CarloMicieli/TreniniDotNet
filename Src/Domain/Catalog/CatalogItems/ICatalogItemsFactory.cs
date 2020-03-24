using System;
using System.Collections.Generic;
using LanguageExt;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItemsFactory
    {
        Validation<Error, ICatalogItem> NewCatalogItem(
            IBrandInfo brand, string itemNumber,
            IScaleInfo scale,
            string powerMethod,
            string? deliveryDate, bool available,
            string description, string? modelDescription, string? prototypeDescription,
            IRollingStock rollingStock);

        Validation<Error, ICatalogItem> NewCatalogItem(
            IBrandInfo brand, string itemNumber,
            IScaleInfo scale,
            string powerMethod,
            string? deliveryDate, bool available,
            string description, string? modelDescription, string? prototypeDescription,
            IReadOnlyList<IRollingStock> rollingStocks);

        Validation<Error, ICatalogItem> HydrateCatalogItem(
            Guid catalogItemId,
            string slug,
            IBrandInfo brand, string itemNumber,
            IScaleInfo scale,
            string powerMethod,
            string? deliveryDate, bool available,
            string description, string? modelDescr, string? prototypeDescr,
            IReadOnlyList<IRollingStock> rollingStocks,
            DateTime lastModifiedAt, int version);
    }
}
