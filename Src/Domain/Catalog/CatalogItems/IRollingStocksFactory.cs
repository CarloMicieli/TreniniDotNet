using System;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface IRollingStocksFactory
    {
        IRollingStock NewLocomotive(
            IRailwayInfo railway,
            Category category,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            string? className, string? roadNumber,
            Couplers? couplers,
            string? livery,
            DccInterface dccInterface, Control control);

        IRollingStock NewPassengerCar(
            IRailwayInfo railway,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            string? typeName,
            Couplers? couplers,
            string? livery,
            PassengerCarType? passengerCarType,
            ServiceLevel? serviceLevel);

        IRollingStock NewFreightCar(
            IRailwayInfo railway,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            string? typeName,
            Couplers? couplers,
            string? livery);

        IRollingStock NewTrain(
            IRailwayInfo railway,
            Category category,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            string? className,
            string? roadNumber,
            Couplers? couplers,
            string? livery,
            DccInterface dccInterface,
            Control control);

        IRollingStock RollingStockWith(
            Guid id,
            IRailwayInfo railway,
            string epoch,
            string category,
            decimal? lengthMillimeters,
            decimal? lengthInches,
            decimal? minRadius,
            string? className = null, string? roadNumber = null, string? typeName = null,
            string? couplers = null,
            string? livery = null,
            string? passengerCarType = null, string? serviceLevel = null,
            string? dccInterface = null, string? control = null);
    }
}
