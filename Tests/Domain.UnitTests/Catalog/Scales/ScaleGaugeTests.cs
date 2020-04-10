using Xunit;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common.Lengths;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScaleGaugeTests
    {
        [Fact]
        public void ScaleGauge_Of_ShouldCreateNewValues()
        {
            ScaleGauge gauge = ScaleGauge.Of(16.5M, 0.65M, TrackGauge.Standard.ToString());

            gauge.Should().NotBeNull();
            gauge.InMillimeters.Should().Be(Gauge.OfMillimiters(16.5M));
            gauge.InInches.Should().Be(Gauge.OfInches(0.65M));
            gauge.TrackGauge.Should().Be(TrackGauge.Standard);
        }

        [Fact]
        public void ScaleGauge_Of_ShouldCreateNewValuesConvertingTheMissingOne()
        {
            ScaleGauge gauge = ScaleGauge.Of(16.5M, MeasureUnit.Millimeters);

            gauge.Should().NotBeNull();
            gauge.InMillimeters.Should().Be(Gauge.OfMillimiters(16.5M));
            gauge.InInches.Should().Be(Gauge.OfInches(0.65M));
            gauge.TrackGauge.Should().Be(TrackGauge.Standard);
        }

        //[Fact]
        //public void ScaleGauge_TryConvert_ShouldCreateGauge_FromValidValues()
        //{
        //    var result = ScaleGauge.TryConvert(16.5M, 0.65M, TrackGauge.Standard.ToString());
        //    result.Match(
        //        Some: gauge =>
        //        {
        //            gauge.Should().NotBeNull();
        //            gauge.InMillimeters.Should().Be(Gauge.OfMillimiters(16.5M));
        //            gauge.InInches.Should().Be(Gauge.OfInches(0.65M));
        //            gauge.TrackGauge.Should().Be(TrackGauge.Standard);
        //        },
        //        None: () => Assert.True(false, "it should never arrive here"));
        //}

        //[Fact]
        //public void ScaleGauge_TryConvert_ShouldFailToCreateGauge_WhenValuesAreNegative()
        //{
        //    var result = ScaleGauge.TryConvert(-16.5M, -0.65M, TrackGauge.Standard.ToString());
        //    result.Match(
        //        Some: gauge => Assert.True(false, "it should never arrive here"),
        //        None: () => Assert.True(true, "it was suppose to arrive here"));
        //}

        //[Fact]
        //public void ScaleGauge_TryCreate_ShouldCreateGauge_FromValidValues()
        //{
        //    var result = ScaleGauge.TryCreate(16.5M, 0.65M, TrackGauge.Standard.ToString());
        //    result.Match(
        //        Succ: gauge =>
        //        {
        //            gauge.Should().NotBeNull();
        //            gauge.InMillimeters.Should().Be(Gauge.OfMillimiters(16.5M));
        //            gauge.InInches.Should().Be(Gauge.OfInches(0.65M));
        //            gauge.TrackGauge.Should().Be(TrackGauge.Standard);
        //        },
        //        Fail: errors => Assert.True(false, "it should never arrive here"));
        //}

        //[Fact]
        //public void ScaleGauge_TryCreate_ShouldFailToCreateGauge_FromInvalidValues()
        //{
        //    var result = ScaleGauge.TryCreate(-16.5M, -0.65M, "invalid");
        //    result.Match(
        //        Succ: gauge => Assert.True(false, "it should never arrive here"),
        //        Fail: errors =>
        //        {
        //            var errorsList = errors.ToList();
        //            errorsList.Should().HaveCount(3);
        //            errorsList.Should().Contain(Error.New($"{-16.5M} millimeters is not a valid value for gauges"));
        //            errorsList.Should().Contain(Error.New($"{-0.65M} inches is not a valid value for gauges"));
        //            errorsList.Should().Contain(Error.New("'invalid' is not a valid track gauge"));
        //        });
        //}
    }
}