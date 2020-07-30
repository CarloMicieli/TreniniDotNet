using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public sealed class PassengerCar : RollingStock
    {
        private PassengerCar() { }

        public PassengerCar(
            RollingStockId rollingStockId,
            Railway railway,
            Category category,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            Couplers? couplers,
            string? typeName,
            string? livery,
            PassengerCarType? passengerCarType,
            ServiceLevel? serviceLevel)
        {
            Id = rollingStockId;
            Railway = railway;
            Category = category;
            Epoch = epoch;
            Length = length;
            MinRadius = minRadius;
            Couplers = couplers;
            Livery = livery;
            TypeName = typeName;
            ServiceLevel = serviceLevel;
            PassengerCarType = passengerCarType;
        }

        public string? TypeName { get; }
        public PassengerCarType? PassengerCarType { get; }
        public ServiceLevel? ServiceLevel { get; }

        public PassengerCar With(
            Railway? railway = null,
            Epoch? epoch = null,
            LengthOverBuffer? length = null,
            MinRadius? minRadius = null,
            Couplers? couplers = null,
            string? typeName = null,
            string? livery = null,
            PassengerCarType? passengerCarType = null,
            ServiceLevel? serviceLevel = null)
        {
            return new PassengerCar(
                Id,
                railway ?? Railway,
                Category,
                epoch ?? Epoch,
                length ?? Length,
                minRadius ?? MinRadius,
                couplers ?? Couplers,
                typeName ?? TypeName,
                livery ?? Livery,
                passengerCarType ?? PassengerCarType,
                serviceLevel ?? ServiceLevel);
        }
    }
}
