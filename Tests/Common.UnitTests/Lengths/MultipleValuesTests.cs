using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Common.Lengths
{
    public class MultipleValuesTests
    {
        private readonly MultipleValues<Length> Subject;

        public MultipleValuesTests()
        {
            Subject = new MultipleValues<Length>(
                MeasureUnit.Inches,
                MeasureUnit.Millimeters,
                (v, mu) => Length.Of(v, mu));
        }

        [Fact]
        public void MultipleLengths_Create_ShouldCreateTwoValues()
        {
            var (len1, len2) = Subject.Create(42.0M, 24.0M);

            len1.Should().Be(Length.OfInches(42.0M));
            len2.Should().Be(Length.OfMillimeters(24.0M));
        }

        [Fact]
        public void MultipleLengths_Create_ShouldCreateTwoValuesConvertingTheMissingOne()
        {
            var (in1, mm1) = Subject.Create(42.0M, null);
            var (in2, mm2) = Subject.Create(null, 24.0M);

            in1.Should().Be(Length.OfInches(42.0M));
            mm1.Should().Be(Length.OfMillimeters(1.65M));
            in2.Should().Be(Length.OfInches(609.60M));
            mm2.Should().Be(Length.OfMillimeters(24.0M));
        }

        [Fact]
        public void MultipleLengths_TryCreate_ShouldCreateTwoValues()
        {
            var result = Subject.TryCreate(42.0M, 24.0M);
            result.Match(
                Succ: pair =>
                {
                    pair.Item1.Should().Be(Length.OfInches(42M));
                    pair.Item2.Should().Be(Length.OfMillimeters(24M));
                },
                Fail: errors => Assert.True(false, "it should never arrive here"));
        }

        [Fact]
        public void MultipleLengths_TryCreate_ShouldFailToCreateTheValues_WhenBothInputAreNull()
        {
            var result = Subject.TryCreate(null, null);
            result.Match(
                Succ: pair => Assert.True(false, "it should never arrive here"),
                Fail: errors =>
                {
                    var errorsList = errors.ToList();
                    errorsList.Should().HaveCount(1);
                    errorsList.Should().Contain(Error.New("Both left and right values are null"));
                });
        }

        [Fact]
        public void MultipleLengths_TryCreate_ShouldFailToCreateTheValues_WhenBothInputAreNegative()
        {
            var result = Subject.TryCreate(-10M, -10M);
            result.Match(
                Succ: pair => Assert.True(false, "it should never arrive here"),
                Fail: errors =>
                {
                    var errorsList = errors.ToList();
                    errorsList.Should().HaveCount(2);
                    errorsList.Should().Contain(Error.New("-10 Inches is not a valid value (negative)"));
                    errorsList.Should().Contain(Error.New("-10 Millimeters is not a valid value (negative)"));
                });
        }
    }
}