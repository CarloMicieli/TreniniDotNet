using System.Linq;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class RollingStockTests
    {
        [Fact]
        public void RollingStock_With_ShouldProduceNewModifiedValues()
        {
            var modified = CatalogSeedData.CatalogItems.Acme_60458().RollingStocks.First()
                .With(prototype: new Prototype(null,"Modified", null, null));

            modified.Should().NotBeNull();
            modified.Prototype!.RoadNumber.Should().Be("Modified");
        }

        [Fact]
        public void RollingStock_Equals_ShouldCheckRollingStockEquality()
        {
            var id = RollingStockId.NewId();
            var rs1 = CatalogSeedData.NewRollingStockWith(id,
                CatalogSeedData.Railways.Fs(),
                Category.PassengerCar,
                Epoch.IV);
            var rs2 = CatalogSeedData.NewRollingStockWith(id,
                CatalogSeedData.Railways.Fs(),
                Category.PassengerCar,
                Epoch.IV);
            var rs3 = CatalogSeedData.NewRollingStockWith(RollingStockId.NewId(),
                CatalogSeedData.Railways.Fs(),
                Category.PassengerCar,
                Epoch.IV);

            rs1.Equals(rs2).Should().BeTrue();
            rs1.Equals(rs3).Should().BeFalse();
        }
    }
}
