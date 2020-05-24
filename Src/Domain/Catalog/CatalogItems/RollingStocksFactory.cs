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

        public IRollingStock NewLocomotive(IRailwayInfo railway, Category category, Epoch epoch, LengthOverBuffer? length,
            string? className, string? roadNumber, string? livery, DccInterface dccInterface, Control control)
        {
            return NewRollingStock(
                railway,
                epoch,
                category,
                length,
                className,
                roadNumber,
                livery: livery,
                dccInterface: dccInterface,
                control: control);
        }

        public IRollingStock NewPassengerCar(IRailwayInfo railway, Epoch epoch, LengthOverBuffer? length,
            string? typeName, string? livery,
            PassengerCarType? passengerCarType, ServiceLevel? serviceLevel)
        {
            return NewRollingStock(
                railway,
                epoch,
                Category.PassengerCar,
                length,
                typeName: typeName,
                passengerCarType: passengerCarType,
                serviceLevel: serviceLevel,
                livery: livery
            );
        }

        public IRollingStock NewFreightCar(IRailwayInfo railway, Epoch era, LengthOverBuffer? length, string? typeName, string? livery)
        {
            return NewRollingStock(
                railway,
                era,
                Category.FreightCar,
                length,
                typeName: typeName,
                livery: livery);
        }

        public IRollingStock NewTrain(IRailwayInfo railway, Category category, Epoch epoch, LengthOverBuffer? length,
            string? className, string? roadNumber, string? livery,
            DccInterface dccInterface, Control control)
        {
            return NewRollingStock(
                railway,
                epoch,
                category,
                length,
                className,
                roadNumber,
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
            string? className = null, string? roadNumber = null, string? typeName = null,
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
                className,
                roadNumber,
                typeName,
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
            string? className = null, string? roadNumber = null, string? typeName = null,
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
                className,
                roadNumber,
                typeName,
                livery,
                passengerCarType,
                serviceLevel,
                dccInterface,
                control);
        }
    }
}
