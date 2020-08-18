using FluentAssertions;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.SharedKernel.Lengths;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScaleGaugeTests
    {
        [Fact]
        public void ScaleGauge_Of_ShouldCreateNewValues()
        {
            ScaleGauge gauge = ScaleGauge.Of(16.5M, 0.65M, TrackGauge.Standard.ToString());

            gauge.Should().NotBeNull();
            gauge.InMillimeters.Should().Be(Gauge.OfMillimeters(16.5M));
            gauge.InInches.Should().Be(Gauge.OfInches(0.65M));
            gauge.TrackGauge.Should().Be(TrackGauge.Standard);
        }

        [Fact]
        public void ScaleGauge_Of_ShouldCreateNewValuesConvertingTheMissingOne()
        {
            ScaleGauge gauge = ScaleGauge.Of(16.5M, MeasureUnit.Millimeters);

            gauge.Should().NotBeNull();
            gauge.InMillimeters.Should().Be(Gauge.OfMillimeters(16.5M));
            gauge.InInches.Should().Be(Gauge.OfInches(0.65M));
            gauge.TrackGauge.Should().Be(TrackGauge.Standard);
        }
    }
}