using FluentAssertions;
using Xunit;

namespace TreniniDotNet.SharedKernel.Lengths
{
    public class MultipleValuesTests
    {
        private readonly MultipleValues<Length> _subject;

        public MultipleValuesTests()
        {
            _subject = new TestMultipleValues(
                MeasureUnit.Inches,
                MeasureUnit.Millimeters);
        }

        [Fact]
        public void MultipleLengths_Create_ShouldCreateTheValueConvertingTheMissing()
        {
            var (len1, len2) = _subject.Create(42.0M, MeasureUnit.Inches);

            len1.Should().Be(Length.OfInches(42.0M));
            len2.Should().Be(Length.OfMillimeters(1066.80M));
        }

        [Fact]
        public void MultipleLengths_Create_ShouldCreateTwoValues()
        {
            var (len1, len2) = _subject.Create(42.0M, 24.0M);

            len1.Should().Be(Length.OfInches(42.0M));
            len2.Should().Be(Length.OfMillimeters(24.0M));
        }

        [Fact]
        public void MultipleLengths_Create_ShouldCreateTwoValuesConvertingTheMissingOne()
        {
            var (in1, mm1) = _subject.Create(0.65M, null);
            var (in2, mm2) = _subject.Create(null, 16.5M);

            in1.Should().Be(Length.OfInches(0.65M));
            mm1.Should().Be(Length.OfMillimeters(16.51M));
            in2.Should().Be(Length.OfInches(0.65M));
            mm2.Should().Be(Length.OfMillimeters(16.5M));
        }

        [Fact]
        public void MultipleLengths_TryCreate_ShouldReturnTrueAndCreateNewValue()
        {
            var result = _subject.TryCreate(11M, 12M, out var ml);

            result.Should().BeTrue();
            ml.Should().NotBeNull();
            ml.Value.Item1.Should().Be(Length.OfInches(11M));
            ml.Value.Item2.Should().Be(Length.OfMillimeters(12M));

        }

        [Fact]
        public void MultipleLengths_TryCreate_ShouldReturnFalse_WhenBothInputAreNull()
        {
            var result = _subject.TryCreate(null, null, out var ml);

            result.Should().BeFalse();
            ml.Should().BeNull();
        }

        [Fact]
        public void MultipleLengths_TryCreate_ShouldReturnFalse_WhenBothInputAreNegative()
        {
            var result = _subject.TryCreate(-10M, -10M, out var ml);

            result.Should().BeFalse();
            ml.Should().BeNull();
        }
    }
}