using System;
using System.Collections.Generic;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItemsFactory
    {
        ICatalogItem CreateNewCatalogItem(
            IBrandInfo brand,
            ItemNumber itemNumber,
            IScaleInfo scale,
            PowerMethod powerMethod,
            IReadOnlyList<IRollingStock> rollingStocks,
            string description,
            string? prototypeDescr,
            string? modelDescr,
            DeliveryDate? deliveryDate,
            bool available);

        ICatalogItem UpdateCatalogItem(
            ICatalogItem item,
            IBrandInfo? brand,
            ItemNumber? itemNumber,
            IScaleInfo? scale,
            PowerMethod? powerMethod,
            string? description,
            string? prototypeDescr,
            string? modelDescr,
            DeliveryDate? deliveryDate,
            bool? available);

        IRollingStock NewLocomotive(
            IRailwayInfo railway, string era, string category,
            LengthOverBuffer? length,
            string? className, string? roadNumber,
            string? dccInterface, string? control);

        IRollingStock NewTrain(
            IRailwayInfo railway, string era, string category,
            LengthOverBuffer? length,
            string? className, string? roadNumber,
            string? dccInterface, string? control);

        IRollingStock NewRollingStock(
            IRailwayInfo railway, string era, string category,
            LengthOverBuffer? length,
            string? typeName);

        // From persistence

        IRollingStock NewRollingStock(
            Guid rollingStockId,
            IRailwayInfo railway,
            string era,
            string category,
            LengthOverBuffer? length,
            string? className, string? roadNumber, string? typeName,
            PassengerCarType? passengerCarType, ServiceLevel? serviceLevel,
            string? dccInterface, string? control);

        ICatalogItem NewCatalogItem(
            Guid catalogItemId,
            string slug,
            IBrandInfo brandInfo, string itemNumber,
            IScaleInfo scaleInfo, string powerMethod, string? deliveryDate, bool available,
            string description, string? modelDescription, string? prototypeDescription,
            IReadOnlyList<IRollingStock> rollingStocks,
            DateTime modified, int version);
    }
}
