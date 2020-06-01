using System;
using NodaTime;
using static TreniniDotNet.Common.Enums.EnumHelpers;
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
            _clock = clock ??
                throw new ArgumentNullException(nameof(clock));
            _guidSource = guidSource ??
                throw new ArgumentNullException(nameof(guidSource));
        }

        public IRollingStock NewLocomotive(IRailwayInfo railway, Category category, Epoch epoch,
            LengthOverBuffer? length, MinRadius? minRadius,
            string? className, string? roadNumber,
            Couplers? couplers,
            string? livery, DccInterface dccInterface, Control control)
        {
            return NewRollingStock(
                railway,
                epoch,
                category,
                length,
                minRadius,
                className,
                roadNumber,
                couplers: couplers,
                livery: livery,
                dccInterface: dccInterface,
                control: control);
        }

        public IRollingStock NewPassengerCar(IRailwayInfo railway, Epoch epoch,
            LengthOverBuffer? length, MinRadius? minRadius,
            string? typeName, Couplers? couplers, string? livery,
            PassengerCarType? passengerCarType, ServiceLevel? serviceLevel)
        {
            return NewRollingStock(
                railway,
                epoch,
                Category.PassengerCar,
                length,
                minRadius,
                typeName: typeName,
                passengerCarType: passengerCarType,
                serviceLevel: serviceLevel,
                livery: livery,
                couplers: couplers
            );
        }

        public IRollingStock NewFreightCar(IRailwayInfo railway, Epoch era,
            LengthOverBuffer? length, MinRadius? minRadius,
            string? typeName,
            Couplers? couplers,
            string? livery)
        {
            return NewRollingStock(
                railway,
                era,
                Category.FreightCar,
                length,
                minRadius,
                couplers: couplers,
                typeName: typeName,
                livery: livery);
        }

        public IRollingStock NewTrain(IRailwayInfo railway, Category category, Epoch epoch,
            LengthOverBuffer? length, MinRadius? minRadius,
            string? className, string? roadNumber,
            Couplers? couplers,
            string? livery,
            DccInterface dccInterface, Control control)
        {
            return NewRollingStock(
                railway,
                epoch,
                category,
                length,
                minRadius,
                className,
                roadNumber,
                couplers: couplers,
                livery: livery,
                dccInterface: dccInterface,
                control: control);
        }

        public IRollingStock RollingStockWith(
            Guid id,
            IRailwayInfo railway,
            string epoch,
            string category,
            decimal? lengthMillimeters,
            decimal? lengthInches,
            decimal? minRadius = null,
            string? className = null, string? roadNumber = null, string? typeName = null,
            string? couplers = null,
            string? livery = null,
            string? passengerCarType = null, string? serviceLevel = null,
            string? dccInterface = null, string? control = null)
        {
            var rollingStockId = new RollingStockId(id);

            return new RollingStock(
                rollingStockId,
                railway,
                RequiredValueFor<Category>(category),
                Epoch.Parse(epoch),
                LengthOverBuffer.CreateOrDefault(lengthInches, lengthMillimeters),
                MinRadius.CreateOrDefault(minRadius),
                new Prototype(className, roadNumber, typeName),
                OptionalValueFor<Couplers>(couplers),
                livery,
                OptionalValueFor<PassengerCarType>(passengerCarType),
                serviceLevel.ToServiceLevelOpt(),
                OptionalValueFor<DccInterface>(dccInterface) ?? DccInterface.None,
                OptionalValueFor<Control>(control) ?? Control.None);
        }

        private IRollingStock NewRollingStock(
            IRailwayInfo railway,
            Epoch epoch,
            Category category,
            LengthOverBuffer? length,
            MinRadius? minRadius = null,
            string? className = null, string? roadNumber = null, string? typeName = null,
            Couplers? couplers = null,
            string? livery = null,
            PassengerCarType? passengerCarType = null, ServiceLevel? serviceLevel = null,
            DccInterface dccInterface = DccInterface.None,
            Control control = Control.None)
        {
            var rollingStockId = new RollingStockId(_guidSource.NewGuid());

            return new RollingStock(
                rollingStockId,
                railway,
                category,
                epoch,
                length,
                minRadius,
                new Prototype(className, roadNumber, typeName),
                couplers,
                livery,
                passengerCarType,
                serviceLevel,
                dccInterface,
                control);
        }
    }
}
