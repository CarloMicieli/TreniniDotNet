using System;
using NodaTime;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class RollingStocksFactory : IRollingStocksFactory
    {
        private readonly IClock _clock;
        private readonly IGuidSource _guidSource;

        public RollingStocksFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock;
            _guidSource = guidSource;
        }

        public IRollingStock NewLocomotive(IRailwayInfo railway, Category category, Epoch epoch, LengthOverBuffer? length,
            string? className, string? roadNumber, DccInterface dccInterface, Control control)
        {
            return NewRollingStock(
                null,
                railway,
                epoch,
                category,
                length,
                className,
                roadNumber,
                dccInterface: dccInterface,
                control: control);
        }

        public IRollingStock NewPassengerCar(IRailwayInfo railway, Epoch epoch, LengthOverBuffer? length, string? typeName,
            PassengerCarType? passengerCarType, ServiceLevel? serviceLevel)
        {
            return NewRollingStock(
                null,
                railway,
                epoch,
                Category.PassengerCar,
                length,
                typeName: typeName,
                passengerCarType: passengerCarType,
                serviceLevel: serviceLevel
            );
        }

        public IRollingStock NewFreightCar(IRailwayInfo railway, Epoch era, LengthOverBuffer? length, string? typeName)
        {
            return NewRollingStock(
                null,
                railway,
                era,
                Category.FreightCar,
                length,
                typeName: typeName);
        }

        public IRollingStock NewTrain(IRailwayInfo railway, Category category, Epoch epoch, LengthOverBuffer? length, string? className,
            string? roadNumber, DccInterface dccInterface, Control control)
        {
            return NewRollingStock(
                null,
                railway,
                epoch,
                category,
                length,
                className,
                roadNumber,
                dccInterface: dccInterface,
                control: control);
        }

        private IRollingStock NewRollingStock(
            Guid? id,
            IRailwayInfo railway,
            Epoch epoch,
            Category category,
            LengthOverBuffer? length,
            string? className = null, string? roadNumber = null, string? typeName = null,
            PassengerCarType? passengerCarType = null, ServiceLevel? serviceLevel = null,
            DccInterface dccInterface = DccInterface.None, Control control = Control.None)
        {
            var rollingStockId =
                (id.HasValue) ? new RollingStockId(id.Value) : new RollingStockId(_guidSource.NewGuid());

            return new RollingStock(
                rollingStockId,
                railway,
                category,
                epoch,
                length,
                className,
                roadNumber,
                typeName,
                passengerCarType,
                serviceLevel,
                dccInterface,
                control);
        }
    }
}
