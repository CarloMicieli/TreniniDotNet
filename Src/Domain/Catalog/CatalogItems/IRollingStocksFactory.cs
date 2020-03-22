using System;
using LanguageExt;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface IRollingStocksFactory
    {
        Validation<Error, IRollingStock> NewLocomotive(
            IRailwayInfo railway, string era, string category,
            decimal? length,
            string? className, string? roadNumber,
            string? dccInterface, string? control);

        Validation<Error, IRollingStock> NewTrain(
            IRailwayInfo railway, string era, string category,
            decimal? length,
            string? className, string? roadNumber,
            string? dccInterface, string? control);

        Validation<Error, IRollingStock> NewRollingStock(
            IRailwayInfo railway, string era, string category,
            decimal? length,
            string? typeName);

        Validation<Error, IRollingStock> HydrateRollingStock(Guid rollingStockId,
            IRailwayInfo railway, string era, string category,
            decimal? length,
            string? className, string? roadNumber, string? typeName,
            string? dccInterface, string? control);
    }
}