using FluentAssertions;
using Xunit;

namespace TreniniDotNet.SharedKernel.Lengths
{
    public class MeasureUnitTests
    {
        [Fact]
        public void MeasureUnit_GetSymbol_ShouldReturnTheMeasureUnitSymbol()
        {
            MeasureUnits.GetSymbol(MeasureUnit.Inches).Should().Be("in");
            MeasureUnits.GetSymbol(MeasureUnit.Miles).Should().Be("mi");
            MeasureUnits.GetSymbol(MeasureUnit.Kilometers).Should().Be("km");
            MeasureUnits.GetSymbol(MeasureUnit.Millimeters).Should().Be("mm");
        }

        [Fact]
        public void MeasureUnit_ToString_ShouldProduceAStringRepresentation()
        {
            MeasureUnit.Inches.ToString(3.14M).Should().Be($"{3.14M} in");
        }

        [Fact]
        public void MeasureUnit_Convert_ShouldReturnOriginalValue_WhenTargetUnitIsTheSame()
        {
            var inches = MeasureUnit.Inches
                .ConvertTo(MeasureUnit.Inches)
                .Convert(16.5M);

            inches.Should().Be(16.5M);
        }

        [Fact]
        public void MeasureUnit_Convert_ShouldConvertBetweenMillimetersAndInches()
        {
            var inches = MeasureUnit.Millimeters
                .ConvertTo(MeasureUnit.Inches)
                .Convert(16.5M);

            inches.Should().Be(0.65M);
        }

        [Fact]
        public void MeasureUnit_Convert_ShouldConvertBetweenInchesAndMillimeters()
        {
            var inches = MeasureUnit.Inches
                .ConvertTo(MeasureUnit.Millimeters)
                .Convert(0.65M, 1);

            inches.Should().Be(16.5M);
        }

        [Fact]
        public void MeasureUnit_Apply_ShouldApplyAFunctionAfterConverter()
        {
            decimal value = 16.5M;
            MeasureUnit mu = MeasureUnit.Millimeters;

            (value, mu).As(MeasureUnit.Inches)
                .Apply<decimal>((vi, mi) =>
                {
                    vi.Should().Be(0.65M);
                    mi.Should().Be(MeasureUnit.Inches);
                    return vi;
                });
        }
    }
}