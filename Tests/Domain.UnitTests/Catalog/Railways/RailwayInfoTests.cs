using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public class RailwayInfoTests
    {
        [Fact]
        public void Railways_ShouldCreateRailwayInfo_FromRailways()
        {
            IRailwayInfo info = DieBahn().ToRailwayInfo();

            info.Should().NotBeNull();
            DieBahn().Name.Should().Be(info.Name);
            DieBahn().Slug.Should().Be(info.Slug);
        }

        [Fact]
        public void Railways_ShouldCreateRailayLabelFromRailwayInfo()
        {
            IRailwayInfo info = DieBahn().ToRailwayInfo();

            info.Should().NotBeNull();
            info.ToLabel().Should().Be("DB");
        }

        private static IRailway DieBahn() => CatalogSeedData.Railways.DieBahn();
    }
}
