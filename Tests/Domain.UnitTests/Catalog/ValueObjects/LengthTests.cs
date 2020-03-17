using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public class LengthTests
    {
        [Fact]
        public void ItShouldCreateAZeroLength()
        {
            var zeroInches = Length.ZeroInches;
            var zeroMms = Length.ZeroMillimeters;

            zeroInches.Should().Be(Length.OfInches(0.0f));
            zeroMms.Should().Be(Length.OfMillimeters(0.0f));
        }

        [Fact]
        public void ItShouldThrowAnExceptionForNegativeLength()
        {
            Assert.Throws<ArgumentException>(() => Length.OfInches(-1.0f));
        }

        [Fact]
        public void ItShouldCreateAMillimetersLengthValues()
        {
            var l = Length.OfMillimeters(123.4f);
            l.ToMillimeters().Should().Be(123.4f);
        }

        [Fact]
        public void ItShouldCreateAInchesLengthValues()
        {
            var l = Length.OfInches(123.4f);
            l.ToInches().Should().Be(123.4f);
        }

        [Fact]
        public void ItShouldCheckLengthEquality()
        {
            var len1 = Length.OfInches(123.4f);
            var len2 = Length.OfInches(123.4f);

            (len1 == len2).Should().BeTrue();
            (len1.Equals(len2)).Should().BeTrue();
        }

        [Fact]
        public void ItShouldCheckLengthInequality()
        {
            var len1 = Length.OfInches(123.4f);
            var len2 = Length.OfInches(321.4f);

            (len1 != len2).Should().BeTrue();
            (len1.Equals(len2)).Should().BeFalse();
            (len1 == len2).Should().BeFalse();
        }

        [Fact]
        public void ItShouldProduceStringRepresentationsForLengthValues()
        {
            var len1 = Length.OfInches(123.4f);
            var len2 = Length.OfMillimeters(123.4f);

            var expectedIn = string.Format("{0}in", 123.4M);
            var expectedMm = string.Format("{0}mm", 123.4M);

            len1.ToString().Should().Be(expectedIn);
            len2.ToString().Should().Be(expectedMm);
        }

        [Fact]
        public void ItShouldAddTwoLengths()
        {
            var tenInches = Length.OfInches(10f);
            var tewntyInches = Length.OfInches(20f);

            (tenInches + tewntyInches).Should().Be(Length.OfInches(30.0f));
        }

        [Fact]
        public void ItShouldAddTwoLengthsWhenTheyHaveDifferentMeasureUnits()
        {
            var tenInches = Length.OfInches(10f);
            var tewntyInches = Length.OfMillimeters(508f);

            (tenInches + tewntyInches).Should().Be(Length.OfInches(30.0f));
        }

        [Fact]
        public void ItShouldSumMultipleLenghtsInInches()
        {
            var lengths = new List<Length>()
            {
                Length.OfInches(10f),
                Length.OfInches(20f),
                Length.OfInches(30f)
            };

            lengths.Sum().Should().Be(Length.OfInches(60.0f));
        }

        [Fact]
        public void ItShouldSumMultipleLenghtsInMillimeters()
        {
            var lengths = new List<Length>()
            {
                Length.OfMillimeters(10f),
                Length.OfMillimeters(20f),
                Length.OfMillimeters(30f)
            };

            lengths.Sum().Should().Be(Length.OfMillimeters(60.0f));
        }

        [Fact]
        public void ItShouldSumMultipleLenghtsWithDifferentMeasureUnits()
        {
            var lengths = new List<Length>()
            {
                Length.OfInches(10f),
                Length.OfMillimeters(508f),
                Length.OfInches(30f)
            };

            lengths.Sum().Should().Be(Length.OfInches(60.0f));
        }

        [Fact]
        public void ItShouldBePossibleToCompareTwoLengths()
        {
            var tenInches = Length.OfInches(10f);
            var tewntyInches = Length.OfInches(20f);

            (tenInches < tewntyInches).Should().BeTrue();
            (tenInches <= tewntyInches).Should().BeTrue();

            (tewntyInches > tenInches).Should().BeTrue();
            (tewntyInches >= tenInches).Should().BeTrue();
        }

        [Fact]
        public void ItShouldBePossibleToCompareTwoLengthsWithDifferentMeasureUnits()
        {
            var tenInches = Length.OfInches(10f);
            var twentyMillimeters = Length.OfMillimeters(20f);

            (tenInches > twentyMillimeters).Should().BeTrue();
            (tenInches >= twentyMillimeters).Should().BeTrue();

            (twentyMillimeters < tenInches).Should().BeTrue();
            (twentyMillimeters <= tenInches).Should().BeTrue();
        }
    }
}
