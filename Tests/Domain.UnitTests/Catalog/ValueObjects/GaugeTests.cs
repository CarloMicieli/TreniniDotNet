using System;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public class GaugeTests
    {
        [Fact]
        public void ItShouldCreateNewGaugeFromAMillimitersValue()
        {
            var halfZero = Gauge.OfMillimiters(16.5f);
            var expected = string.Format("{0} mm", 16.5M);

            Assert.Equal(expected, halfZero.ToString());
        }

        [Fact]
        public void ItShouldCreateNewGaugeFromAnInchesValue()
        {
            var halfZero = Gauge.OfInches(0.65f);
            var expected = string.Format("{0} in", 0.65M);

            Assert.Equal(expected, halfZero.ToString());
        }

        [Fact]
        public void ItShouldThrowAnExceptionForZeroGauges()
        {
            Assert.Throws<ArgumentException>(() => Gauge.OfMillimiters(0.0f));
        }

        [Fact]
        public void ItShouldThrowAnExceptionForNegativeValues()
        {
            Assert.Throws<ArgumentException>(() => Gauge.OfMillimiters(-1.0f));
        }

        [Fact]
        public void ItShouldCheckGaugesEquality()
        {
            var halfZero1 = Gauge.OfMillimiters(16.5f);
            var halfZero2 = Gauge.OfMillimiters(16.5f);
            Assert.True(halfZero1 == halfZero2);
            Assert.True(halfZero1.Equals(halfZero2));
        }

        [Fact]
        public void ItShouldCheckGaugesInequality()
        {
            var halfZero = Gauge.OfMillimiters(16.5f);
            var zero = Gauge.OfMillimiters(32f);
            Assert.True(zero != halfZero);
            Assert.False(halfZero.Equals(zero));
            Assert.False(halfZero.Equals("it fails"));
        }

        [Fact]
        public void ItShouldCompareTwoGauges()
        {
            var halfZero = Gauge.OfMillimiters(16.5f);
            var zero = Gauge.OfMillimiters(32f);

            Assert.True(halfZero.CompareTo(halfZero) == 0);
            Assert.True(zero.CompareTo(halfZero) > 0);
            Assert.True(halfZero.CompareTo(zero) < 0);
            Assert.True(halfZero < zero);
            Assert.False(halfZero >= zero);
            Assert.True(zero > halfZero);
            Assert.False(zero <= halfZero);
        }
    }
}
