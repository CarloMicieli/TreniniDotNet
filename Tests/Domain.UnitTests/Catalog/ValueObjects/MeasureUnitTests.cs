using Xunit;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public class MeasureUnitTests
    {
        [Fact]
        public void ItShouldApplyAFunctionDependingOnTheMeasureUnit()
        {
            float res1 = MeasureUnit.Inches.Apply(42,
                v => v * 2,
                v => v * 3);
            float res2 = MeasureUnit.Millimeters.Apply(42,
                v => v * 2,
                v => v * 3);

            Assert.Equal(42.0f * 2, res1);
            Assert.Equal(42.0f * 3, res2);
        }

        [Fact]
        public void ItShouldTakeTheValueIfTheMeasureUnitsAreEquals()
        {
            float res = MeasureUnit.Inches.GetValueOrConvert(
                () => (42.0f, MeasureUnit.Inches));
            Assert.Equal(42.0f, res);
        }

        [Fact]
        public void ItShouldConvertTheValueIfTheMeasureUnitsAreDifferents()
        {
            float res = MeasureUnit.Inches.GetValueOrConvert(
                () => (42.0f, MeasureUnit.Millimeters));

            Assert.Equal(1.65f, res, 2);
        }
    }
}
