using Xunit;
using FluentAssertions;
using TreniniDotNet.Common.Lengths;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public class RailwayGaugeTests
    {
        [Fact]
        public void RailwayGauge_Create_ShouldCreateNewRailwayGauges()
        {
            var railwayGauge = RailwayGauge.Create(TrackGauge.Standard.ToString(), 0.65M, 16.5M);

            railwayGauge.Should().NotBeNull();
            railwayGauge.Inches.Should().Be(Length.OfInches(0.65M));
            railwayGauge.Millimeters.Should().Be(Length.OfMillimeters(16.5M));
        }
    }
}