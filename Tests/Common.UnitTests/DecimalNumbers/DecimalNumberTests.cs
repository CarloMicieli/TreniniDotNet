using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Common.DecimalNumbers
{
    public class DecimalNumberTests
    {
        [Fact]
        public void ItShouldCreateEmptyDecimalNumbers()
        {
            var emptyVal = DecimalNumber.NaN;
            emptyVal.IsNaN.Should().BeTrue();
        }

        [Fact]
        public void ItShouldProduceAStringRepresentationForEmptyDecimalNumbers()
        {
            var emptyVal = DecimalNumber.NaN;
            emptyVal.ToString().Should().Be("NaN");
        }

        [Fact]
        public void ItShouldCreateANewDecimalNumbers()
        {
            var value = DecimalNumber.Of(3.14f, Digits.Two);
            value.ToString().Should().Be("(value=314, factor=100)");
        }

        [Fact]
        public void ItShouldCreateADecimalNumbersWithDifferentNumberOfDigits()
        {
            var value1 = DecimalNumber.Of(3.14f, Digits.One);
            var value3 = DecimalNumber.Of(3.14f, Digits.Three);
            var value4 = DecimalNumber.Of(3.14f, Digits.Four);

            value1.ToString().Should().Be("(value=31, factor=10)");
            value3.ToString().Should().Be("(value=3140, factor=1000)");
            value4.ToString().Should().Be("(value=31400, factor=10000)");
        }

        [Fact]
        public void ItShouldProduceFloatNumbersFromDecimalNumbers()
        {
            var value = DecimalNumber.Of(3.14f, Digits.Two);

            var successful = value.TryGetFloat(out float f);
            successful.Should().BeTrue();
            f.Should().Be(3.14f);
        }

        [Fact]
        public void ItShouldProduceDoubleNumbersFromDecimalNumbers()
        {
            var value = DecimalNumber.Of(3.14f, Digits.Two);

            var successful = value.TryGetDouble(out double d);

            successful.Should().BeTrue();
            d.Should().Be(3.14);
        }

        [Fact]
        public void ItShouldCompareTwoDecimalNumbers()
        {
            var none = DecimalNumber.NaN;
            var fortyTwo1 = DecimalNumber.Of(42, Digits.Zero);
            var fortyTwo2 = DecimalNumber.Of(42, Digits.Zero);

            (fortyTwo1 == fortyTwo2).Should().BeTrue();
            (fortyTwo1.Equals(fortyTwo2)).Should().BeTrue();
            (DecimalNumber.NaN == none).Should().BeTrue();
            (none.Equals(none)).Should().BeTrue();
        }

        [Fact]
        public void ItShouldCheckWhetherTwoDecimalNumbersAreDifferent()
        {
            var none = DecimalNumber.NaN;
            var fortyTwo = DecimalNumber.Of(42, Digits.Zero);
            var pi = DecimalNumber.Of(3.14f, Digits.Two);

            (pi == fortyTwo).Should().BeFalse();
            (pi.Equals(fortyTwo)).Should().BeFalse();
            (none != fortyTwo).Should().BeTrue();
            (pi != fortyTwo).Should().BeTrue();
            (none.Equals(fortyTwo)).Should().BeFalse();
            (fortyTwo.Equals(none)).Should().BeFalse();
        }
    }
}
