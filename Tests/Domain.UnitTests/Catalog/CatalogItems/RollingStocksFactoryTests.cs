using System;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common.Lengths;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.Common.Uuid.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class RollingStocksFactoryTests
    {
        private IRollingStocksFactory Factory { get; }

        public RollingStocksFactoryTests()
        {
            Factory = new RollingStocksFactory(
                new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0)),
                FakeGuidSource.NewSource(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af"))
            );
        }

        [Fact]
        public void RollingStocksFactory_NewFreightCar_ShouldCreateNewValues()
        {
            var newFreightCar = Factory.NewFreightCar(
                Fs(),
                Epoch.VI,
                LengthOverBuffer.OfMillimeters(135M),
                MinRadius.OfMillimeters(360),
                "Sdggmrs T2000",
                Couplers.Nem352,
                "red");

            newFreightCar.Should().NotBeNull();
            newFreightCar.Epoch.Should().Be(Epoch.VI);
            newFreightCar.Category.Should().Be(Category.FreightCar);
            newFreightCar.Prototype!.TypeName.Should().Be("Sdggmrs T2000");
            newFreightCar.Length?.Millimeters.Should().Be(Length.OfMillimeters(135M));
            newFreightCar.Livery.Should().Be("red");
            newFreightCar.MinRadius.Should().Be(MinRadius.OfMillimeters(360));
            newFreightCar.Couplers.Should().Be(Couplers.Nem352);
        }

        [Fact]
        public void RollingStocksFactory_NewPassengerCar_ShouldCreateNewValues()
        {
            var newPassengerCar = Factory.NewPassengerCar(
                Fs(),
                Epoch.IV,
                LengthOverBuffer.OfMillimeters(242M),
                MinRadius.OfMillimeters(360),
                "Tipo 1921",
                "Serie",
                Couplers.Nem352,
                "red",
                PassengerCarType.CompartmentCoach,
                ServiceLevel.SecondClass);

            newPassengerCar.Should().NotBeNull();
            newPassengerCar.Category.Should().Be(Category.PassengerCar);
            newPassengerCar.Railway.Should().Be(Fs());
            newPassengerCar.Epoch.Should().Be(Epoch.IV);
            newPassengerCar.Prototype!.TypeName.Should().Be("Tipo 1921");
            newPassengerCar.Prototype!.Series.Should().Be("Serie");
            newPassengerCar.Livery.Should().Be("red");
            newPassengerCar.PassengerCarType.Should().Be(PassengerCarType.CompartmentCoach);
            newPassengerCar.ServiceLevel.Should().Be(ServiceLevel.SecondClass);
            newPassengerCar.Length?.Millimeters.Should().Be(Length.OfMillimeters(242M));
            newPassengerCar.MinRadius.Should().Be(MinRadius.OfMillimeters(360));
            newPassengerCar.Couplers.Should().Be(Couplers.Nem352);
        }

        [Fact]
        public void RollingStocksFactory_NewLocomotive_ShouldCreateNewLocomotives()
        {
            var newLocomotive = Factory.NewLocomotive(
                Fs(),
                Category.ElectricLocomotive,
                Epoch.IV,
                LengthOverBuffer.OfMillimeters(210M),
                MinRadius.OfMillimeters(360),
                "E 656",
                "E 656 210",
                "3 serie",
                Couplers.Nem352,
                "blue",
                "Milano Smistamento",
                DccInterface.Nem652,
                Control.DccReady);

            newLocomotive.Should().NotBeNull();
            newLocomotive.Railway.Should().Be(Fs());
            newLocomotive.Category.Should().Be(Category.ElectricLocomotive);
            newLocomotive.Epoch.Should().Be(Epoch.IV);
            newLocomotive.Length?.Millimeters.Should().Be(Length.OfMillimeters(210M));
            newLocomotive.Prototype!.ClassName.Should().Be("E 656");
            newLocomotive.Prototype!.RoadNumber.Should().Be("E 656 210");
            newLocomotive.Prototype!.Series.Should().Be("3 serie");
            newLocomotive.Livery.Should().Be("blue");
            newLocomotive.DccInterface.Should().Be(DccInterface.Nem652);
            newLocomotive.Control.Should().Be(Control.DccReady);
            newLocomotive.MinRadius.Should().Be(MinRadius.OfMillimeters(360));
            newLocomotive.Couplers.Should().Be(Couplers.Nem352);
            newLocomotive.Depot.Should().Be("Milano Smistamento");
        }

        [Fact]
        public void RollingStocksFactory_NewTrain_ShouldCreateNewValues()
        {
            var newTrain = Factory.NewTrain(
                Fs(),
                Category.ElectricMultipleUnit,
                Epoch.VI,
                LengthOverBuffer.OfMillimeters(2321M),
                MinRadius.OfMillimeters(360),
                "Etr 400",
                "",
                series: "Prototype",
                Couplers.Nem352,
                livery: "red",
                depot: "Milano Centrale",
                DccInterface.Mtc21,
                Control.DccReady
            );

            newTrain.Should().NotBeNull();
            newTrain.Railway.Should().Be(Fs());
            newTrain.Category.Should().Be(Category.ElectricMultipleUnit);
            newTrain.Epoch.Should().Be(Epoch.VI);
            newTrain.Length?.Millimeters.Should().Be(Length.OfMillimeters(2321M));
            newTrain.Prototype!.ClassName.Should().Be("Etr 400");
            newTrain.Prototype!.Series.Should().Be("Prototype");
            newTrain.Livery.Should().Be("red");
            newTrain.DccInterface.Should().Be(DccInterface.Mtc21);
            newTrain.Control.Should().Be(Control.DccReady);
            newTrain.MinRadius.Should().Be(MinRadius.OfMillimeters(360));
            newTrain.Couplers.Should().Be(Couplers.Nem352);
            newTrain.Depot.Should().Be("Milano Centrale");
        }

        [Fact]
        public void RollingStocksFactory_RollingStockWith_ShouldCreateNewValues()
        {
            var rollingStockId = Guid.NewGuid();
            var rs = Factory.RollingStockWith(
                rollingStockId,
                Fs(),
                Epoch.III.ToString(),
                Category.ElectricLocomotive.ToString(),
                210M,
                null,
                360M,
                "Class name",
                "Road Number",
                "Type name",
                "Series",
                "Nem352",
                "red",
                "Depot name",
                PassengerCarType.Observation.ToString(),
                ServiceLevel.SecondClass.ToString(),
                DccInterface.Nem651.ToString(),
                Control.DccReady.ToString());

            rs.Should().NotBeNull();
            rs.Id.Should().Be(new RollingStockId(rollingStockId));
            rs.Railway.Should().Be(Fs());
            rs.Epoch.Should().Be(Epoch.III);
            rs.Category.Should().Be(Category.ElectricLocomotive);
            rs.Length?.Millimeters.Should().Be(Length.OfMillimeters(210M));
            rs.Prototype!.ClassName.Should().Be("Class name");
            rs.Prototype!.RoadNumber.Should().Be("Road Number");
            rs.Prototype!.TypeName.Should().Be("Type name");
            rs.Livery.Should().Be("red");
            rs.PassengerCarType.Should().Be(PassengerCarType.Observation);
            rs.ServiceLevel.Should().Be(ServiceLevel.SecondClass);
            rs.DccInterface.Should().Be(DccInterface.Nem651);
            rs.Control.Should().Be(Control.DccReady);
            rs.MinRadius.Should().Be(MinRadius.OfMillimeters(360));
            rs.Couplers.Should().Be(Couplers.Nem352);
            rs.Depot.Should().Be("Depot name");
            rs.Prototype!.Series.Should().Be("Series");
        }

        private static IRailwayInfo Fs() => CatalogSeedData.Railways.Fs();
    }
}
