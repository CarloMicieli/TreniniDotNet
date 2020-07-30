using System;
using FluentAssertions;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public class RollingStocksFactoryTests
    {
        private RollingStocksFactory Factory { get; }
        private readonly RollingStockId _expectedItemId = new RollingStockId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af"));

        public RollingStocksFactoryTests()
        {
            Factory = new RollingStocksFactory(
                FakeGuidSource.NewSource(_expectedItemId));
        }

        [Fact]
        public void RollingStocksFactory_CreateLocomotive_ShouldCreateNewValues()
        {
            var locomotive = Factory.CreateLocomotive(
                CatalogSeedData.Railways.Fs(),
                Category.ElectricLocomotive,
                Epoch.IV,
                LengthOverBuffer.OfMillimeters(210),
                MinRadius.OfMillimeters(360),
                Prototype.OfLocomotive("E656", "E656 210"),
                Couplers.Nem352,
                "blu / grigio",
                "Milano Centrale",
                DccInterface.Nem652,
                Control.DccReady);

            locomotive.Should().NotBeNull();
            locomotive.Id.Should().Be(_expectedItemId);
            locomotive.Railway.Should().Be(CatalogSeedData.Railways.Fs());
            locomotive.Category.Should().Be(Category.ElectricLocomotive);
            locomotive.Epoch.Should().Be(Epoch.IV);
            locomotive.Length.Should().Be(LengthOverBuffer.OfMillimeters(210));
            locomotive.MinRadius.Should().Be(MinRadius.OfMillimeters(360));
            locomotive.Couplers.Should().Be(Couplers.Nem352);
            locomotive.Livery.Should().Be("blu / grigio");
            locomotive.Depot.Should().Be("Milano Centrale");
            locomotive.DccInterface.Should().Be(DccInterface.Nem652);
            locomotive.Control.Should().Be(Control.DccReady);
        }

        [Fact]
        public void RollingStocksFactory_CreateTrain_ShouldCreateNewValues()
        {
            var train = Factory.CreateTrain(
                CatalogSeedData.Railways.Fs(),
                Category.ElectricMultipleUnit,
                Epoch.IV,
                LengthOverBuffer.OfMillimeters(210),
                MinRadius.OfMillimeters(360),
                Couplers.Nem352,
                "Etr 220",
                "blu / grigio",
                DccInterface.Nem652,
                Control.DccReady);

            train.Should().NotBeNull();
            train.Id.Should().Be(_expectedItemId);
            train.Railway.Should().Be(CatalogSeedData.Railways.Fs());
            train.Category.Should().Be(Category.ElectricMultipleUnit);
            train.Epoch.Should().Be(Epoch.IV);
            train.Length.Should().Be(LengthOverBuffer.OfMillimeters(210));
            train.MinRadius.Should().Be(MinRadius.OfMillimeters(360));
            train.Couplers.Should().Be(Couplers.Nem352);
            train.TypeName.Should().Be("Etr 220");
            train.Livery.Should().Be("blu / grigio");
            train.DccInterface.Should().Be(DccInterface.Nem652);
            train.Control.Should().Be(Control.DccReady);
        }

        [Fact]
        public void RollingStocksFactory_CreatePassengerCar_ShouldCreateNewValues()
        {
            var passengerCar = Factory.CreatePassengerCar(
                CatalogSeedData.Railways.Fs(),
                Epoch.IV,
                LengthOverBuffer.OfMillimeters(210),
                MinRadius.OfMillimeters(360),
                Couplers.Nem352,
                "UIC-X",
                "grigio ardesia",
                PassengerCarType.OpenCoach,
                ServiceLevel.SecondClass
            );

            passengerCar.Should().NotBeNull();
            passengerCar.Id.Should().Be(_expectedItemId);
            passengerCar.Category.Should().Be(Category.PassengerCar);
            passengerCar.Railway.Should().Be(CatalogSeedData.Railways.Fs());
            passengerCar.Epoch.Should().Be(Epoch.IV);
            passengerCar.Length.Should().Be(LengthOverBuffer.OfMillimeters(210));
            passengerCar.MinRadius.Should().Be(MinRadius.OfMillimeters(360));
            passengerCar.Couplers.Should().Be(Couplers.Nem352);
            passengerCar.Livery.Should().Be("grigio ardesia");
            passengerCar.ServiceLevel.Should().Be(ServiceLevel.SecondClass);
            passengerCar.PassengerCarType.Should().Be(PassengerCarType.OpenCoach);
            passengerCar.TypeName.Should().Be("UIC-X");
        }

        [Fact]
        public void RollingStocksFactory_CreateFreightCar_ShouldCreateNewValues()
        {
            var freightCar = Factory.CreateFreightCar(
                CatalogSeedData.Railways.Fs(),
                Epoch.IV,
                LengthOverBuffer.OfMillimeters(210),
                MinRadius.OfMillimeters(360),
                Couplers.Nem352,
                "Gondola",
                "grigio ardesia"
            );

            freightCar.Should().NotBeNull();
            freightCar.Id.Should().Be(_expectedItemId);
            freightCar.Category.Should().Be(Category.FreightCar);
            freightCar.Railway.Should().Be(CatalogSeedData.Railways.Fs());
            freightCar.Epoch.Should().Be(Epoch.IV);
            freightCar.Length.Should().Be(LengthOverBuffer.OfMillimeters(210));
            freightCar.MinRadius.Should().Be(MinRadius.OfMillimeters(360));
            freightCar.Couplers.Should().Be(Couplers.Nem352);
            freightCar.Livery.Should().Be("grigio ardesia");
            freightCar.TypeName.Should().Be("Gondola");
        }
    }
}
