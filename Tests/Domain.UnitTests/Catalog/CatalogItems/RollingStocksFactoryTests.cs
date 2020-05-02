using System;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common.Lengths;
using TreniniDotNet.Domain.Catalog.Railways;
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
                "Sdggmrs T2000");

            newFreightCar.Should().NotBeNull();
            newFreightCar.Epoch.Should().Be(Epoch.VI);
            newFreightCar.Category.Should().Be(Category.FreightCar);
            newFreightCar.TypeName.Should().Be("Sdggmrs T2000");
            newFreightCar.Length?.Millimeters.Should().Be(Length.OfMillimeters(135M));
        }

        [Fact]
        public void RollingStocksFactory_NewPassengerCar_ShouldCreateNewValues()
        {
            var newPassengerCar = Factory.NewPassengerCar(
                Fs(),
                Epoch.IV,
                LengthOverBuffer.OfMillimeters(242M),
                "Tipo 1921",
                PassengerCarType.CompartmentCoach,
                ServiceLevel.SecondClass);

            newPassengerCar.Should().NotBeNull();
            newPassengerCar.Category.Should().Be(Category.PassengerCar);
            newPassengerCar.Railway.Should().Be(Fs());
            newPassengerCar.Epoch.Should().Be(Epoch.IV);
            newPassengerCar.TypeName.Should().Be("Tipo 1921");
            newPassengerCar.PassengerCarType.Should().Be(PassengerCarType.CompartmentCoach);
            newPassengerCar.ServiceLevel.Should().Be(ServiceLevel.SecondClass);
            newPassengerCar.Length?.Millimeters.Should().Be(Length.OfMillimeters(242M));
        }

        [Fact]
        public void RollingStocksFactory_NewLocomotive_ShouldCreateNewLocomotives()
        {
            var newLocomotive = Factory.NewLocomotive(
                Fs(),
                Category.ElectricLocomotive,
                Epoch.IV,
                LengthOverBuffer.OfMillimeters(210M),
                "E 656",
                "E 656 210",
                DccInterface.Nem652,
                Control.DccReady);

            newLocomotive.Should().NotBeNull();
            newLocomotive.Railway.Should().Be(Fs());
            newLocomotive.Category.Should().Be(Category.ElectricLocomotive);
            newLocomotive.Epoch.Should().Be(Epoch.IV);
            newLocomotive.Length?.Millimeters.Should().Be(Length.OfMillimeters(210M));
            newLocomotive.ClassName.Should().Be("E 656");
            newLocomotive.RoadNumber.Should().Be("E 656 210");
            newLocomotive.DccInterface.Should().Be(DccInterface.Nem652);
            newLocomotive.Control.Should().Be(Control.DccReady);
        }

        [Fact]
        public void RollingStocksFactory_NewTrain_ShouldCreateNewValues()
        {
            var rs = Factory.NewTrain(
                Fs(),
                Category.ElectricMultipleUnit,
                Epoch.VI,
                LengthOverBuffer.OfMillimeters(2321M),
                "Etr 400",
                "",
                DccInterface.Mtc21,
                Control.DccReady
            );

            rs.Should().NotBeNull();
            rs.Railway.Should().Be(Fs());
            rs.Category.Should().Be(Category.ElectricMultipleUnit);
            rs.Epoch.Should().Be(Epoch.VI);
            rs.Length?.Millimeters.Should().Be(Length.OfMillimeters(2321M));
            rs.ClassName.Should().Be("Etr 400");
            rs.DccInterface.Should().Be(DccInterface.Mtc21);
            rs.Control.Should().Be(Control.DccReady);
        }

        private static IRailwayInfo Fs() => CatalogSeedData.Railways.Fs();
    }
}
