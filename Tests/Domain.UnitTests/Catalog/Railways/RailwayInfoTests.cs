using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public class RailwayInfoTests
    {
        [Fact]
        public void ItShouldCreateRailwayInfo_FromRailways()
        {
            IRailwayInfo info = DieBahn().ToRailwayInfo();
            Assert.NotNull(info);
            Assert.Equal(DieBahn().Country, info.Country);
            Assert.Equal(DieBahn().Name, info.Name);
            Assert.Equal(DieBahn().Slug, info.Slug);
        }

        [Fact]
        public void ItShouldCreateRailayLabelFromRailwayInfo()
        {
            IRailwayInfo info = DieBahn().ToRailwayInfo();
            Assert.NotNull(info);
            Assert.Equal("DB", info.ToLabel());
        }

        private static IRailway DieBahn()
        {
            return new Railway(
                RailwayId.NewId(),
                Slug.Of("die-bahn"),
                "DB",
                "die Bahn",
                "DE",
                null,
                null,
                RailwayStatus.Active, 
                DateTime.UtcNow, 
                1);
        }
    }
}