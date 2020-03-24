using Xunit;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using FluentAssertions;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public class RailwayTests
    {
        [Fact]
        public void ItShouldCheckRailwaysEquality()
        {
            var db1 = DieBahn();
            var db2 = DieBahn();

            //(db1 == db2).Should().BeTrue();
            (db1.Equals(db2)).Should().BeTrue();
        }

        [Fact]
        public void ItShouldCheckRailwaysInequality()
        {
            var db = DieBahn();
            var fs = Fs();

            (db != fs).Should().BeTrue();
            (db.Equals(fs)).Should().BeFalse();
        }

        [Fact]
        public void Railways_ShouldProduce_AStringRepresentation()
        {
            DieBahn().ToString().Should().Be("DB");
            DieBahn().ToLabel().Should().Be("DB");
        }

        private static IRailway DieBahn() => CatalogSeedData.Railways.DieBahn();

        private static IRailway Fs() => CatalogSeedData.Railways.Fs();
    }
}
