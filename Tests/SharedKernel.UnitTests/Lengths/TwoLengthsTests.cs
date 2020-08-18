using FluentAssertions;
using Xunit;

namespace TreniniDotNet.SharedKernel.Lengths
{
    public class TwoLengthsTests
    {
        private readonly TwoLengths _inchesMillimeters;

        public TwoLengthsTests()
        {
            _inchesMillimeters = new TwoLengths(MeasureUnit.Inches, MeasureUnit.Millimeters);
        }

        [Fact]
        public void TwoLengths_ShouldCreateTwoLengths()
        {
            decimal inches = 0.65M;
            decimal mm = 16.5M;

            var (lIn, lMM) = _inchesMillimeters.Create(inches, mm);
            lIn.Should().Be(Length.OfInches(inches));
            lMM.Should().Be(Length.OfMillimeters(mm));
        }
    }
}
