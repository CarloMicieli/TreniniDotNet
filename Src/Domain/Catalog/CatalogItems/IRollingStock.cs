using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface IRollingStock
    {
        RollingStockId RollingStockId { get; }

        IRailwayInfo Railway { get; }

        Category Category { get; }

        Epoch Epoch { get; }

        LengthOverBuffer? Length { get; }

        Prototype? Prototype { get; }

        Couplers? Couplers { get; }

        string? Livery { get; }

        PassengerCarType? PassengerCarType { get; }

        ServiceLevel? ServiceLevel { get; }

        DccInterface DccInterface { get; }

        Control Control { get; }

        IRollingStock With(
            IRailwayInfo? railway = null,
            Category? category = null,
            Epoch? epoch = null,
            LengthOverBuffer? length = null,
            Prototype? prototype = null,
            Couplers? couplers = null,
            string? livery = null,
            PassengerCarType? passengerCarType = null, ServiceLevel? serviceLevel = null,
            DccInterface? dccInterface = null, Control? control = null)
        {
            return new RollingStock(
                RollingStockId,
                railway ?? Railway,
                category ?? Category,
                epoch ?? Epoch,
                length ?? Length,
                prototype ?? Prototype,
                couplers ?? Couplers,
                livery ?? Livery,
                passengerCarType ?? PassengerCarType,
                serviceLevel ?? ServiceLevel,
                dccInterface ?? DccInterface,
                control ?? Control);
        }
    }
}
