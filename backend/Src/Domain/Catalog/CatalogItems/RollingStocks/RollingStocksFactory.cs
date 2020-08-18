using System;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public sealed class RollingStocksFactory : EntityFactory<RollingStockId, RollingStock>
    {
        public RollingStocksFactory(IGuidSource guidSource)
            : base(guidSource)
        {
        }

        public Locomotive CreateLocomotive(
            RailwayRef railway,
            Category category,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            Prototype? prototype,
            Couplers? couplers,
            string? livery,
            string? depot,
            DccInterface dccInterface,
            Control control)
        {
            return new Locomotive(
                NewId(id => new RollingStockId(id)),
                railway,
                category,
                epoch,
                length,
                minRadius,
                prototype,
                couplers,
                livery,
                depot,
                dccInterface,
                control);
        }

        public FreightCar CreateFreightCar(
            RailwayRef railway,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            Couplers? couplers,
            string? typeName,
            string? livery)
        {
            return new FreightCar(
                NewId(id => new RollingStockId(id)),
                railway,
                Category.FreightCar,
                epoch,
                length,
                minRadius,
                couplers,
                typeName,
                livery);
        }

        public PassengerCar CreatePassengerCar(
            RailwayRef railway,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            Couplers? couplers,
            string? typeName,
            string? livery,
            PassengerCarType? passengerCarType,
            ServiceLevel? serviceLevel)
        {
            return new PassengerCar(
                NewId(id => new RollingStockId(id)),
                railway,
                Category.PassengerCar,
                epoch,
                length,
                minRadius,
                couplers,
                typeName,
                livery,
                passengerCarType,
                serviceLevel);
        }

        public Train CreateTrain(
            RailwayRef railway,
            Category category,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            Couplers? couplers,
            string? typeName,
            string? livery,
            DccInterface dccInterface,
            Control control)
        {
            return new Train(
                NewId(id => new RollingStockId(id)),
                railway,
                category,
                epoch,
                length,
                minRadius,
                couplers,
                typeName,
                livery,
                dccInterface,
                control);
        }
    }
}
