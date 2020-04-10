using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Common.Lengths
{
    public class TwoLengthsTests
    {
        private readonly TwoLengths InchesMillimeters;

        public TwoLengthsTests()
        {
            InchesMillimeters = new TwoLengths(MeasureUnit.Inches, MeasureUnit.Millimeters);
        }
            
        [Fact]
        public void TwoLengths_ShouldCreateTwoLengths()
        {
            decimal inches = 0.65M;
            decimal mm = 16.5M;

            var (lIn, lMM) = InchesMillimeters.Create(inches, mm);
            lIn.Should().Be(Length.OfInches(inches));
            lMM.Should().Be(Length.OfMillimeters(mm));
        }
    }
}
