using System;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public class RatioTests
    {
        [Fact]
        public void ItShouldCreateANewRationFromFloatValues()
        {
            var halfZero = Ratio.Of(87.0f);
            var zero = Ratio.Of(43.5f);

            var expectedZero = string.Format("1:{0}", 43.5M);

            Assert.Equal("1:87", halfZero.ToString());
            Assert.Equal(expectedZero, zero.ToString());
        }

        [Fact]
        public void ItShouldThrowAnExceptionForZeroRatios()
        {
            Assert.Throws<ArgumentException>(() => Ratio.Of(0.0f));
        }

        [Fact]
        public void ItShouldThrowAnExceptionForNegativeRatios()
        {
            Assert.Throws<ArgumentException>(() => Ratio.Of(-10.0f));
        }

        [Fact]
        public void ItShouldCheckForRatioEquality()
        {
            var halfZero1 = Ratio.Of(87.0f);
            var halfZero2 = Ratio.Of(87.0f);
            Assert.True(halfZero1 == halfZero2);
            Assert.True(halfZero1.Equals(halfZero2));
        }

        [Fact]
        public void ItShouldCheckForRatioInequality()
        {
            var halfZero = Ratio.Of(87.0f);
            var zero = Ratio.Of(43.5f);
            Assert.True(halfZero != zero);
            Assert.True(!halfZero.Equals(zero));
            Assert.False(halfZero == zero);
            Assert.False(halfZero.Equals(zero));
            Assert.False(halfZero.Equals("it fails"));
        }
    }
}
