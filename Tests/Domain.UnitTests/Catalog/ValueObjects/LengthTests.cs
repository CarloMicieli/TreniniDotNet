using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public class LengthTests
    {
        [Fact]
        public void ItShouldCreateAZeroLength()
        {
            var zeroInches = Length.ZeroInches;
            var zeroMms = Length.ZeroMillimeters;

            Assert.Equal(Length.OfInches(0.0f), zeroInches);
            Assert.Equal(Length.OfMillimeters(0.0f), zeroMms);
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
            Assert.Equal(123.4f, l.ToMillimeters());
        }

        [Fact]
        public void ItShouldCreateAInchesLengthValues()
        {
            var l = Length.OfInches(123.4f);
            Assert.Equal(123.4f, l.ToInches());
        }

        [Fact]
        public void ItShouldCheckLengthEquality()
        {
            var len1 = Length.OfInches(123.4f);
            var len2 = Length.OfInches(123.4f);

            Assert.True(len1 == len2);
            Assert.True(len1.Equals(len2));
        }

        [Fact]
        public void ItShouldCheckLengthInequality()
        {
            var len1 = Length.OfInches(123.4f);
            var len2 = Length.OfInches(321.4f);

            Assert.True(len1 != len2);
            Assert.False(len1.Equals(len2));
            Assert.False(len1 == len2);
        }

        [Fact]
        public void ItShouldProduceStringRepresentationsForLengthValues()
        {
            var len1 = Length.OfInches(123.4f);
            var len2 = Length.OfMillimeters(123.4f);

            var expectedIn = string.Format("{0}in", 123.4M);
            var expectedMm = string.Format("{0}mm", 123.4M);

            Assert.Equal(expectedIn, len1.ToString());
            Assert.Equal(expectedMm, len2.ToString());
        }

        [Fact]
        public void ItShouldAddTwoLengths()
        {
            var tenInches = Length.OfInches(10f);
            var tewntyInches = Length.OfInches(20f);

            Assert.Equal(Length.OfInches(30.0f), tenInches + tewntyInches);
        }

        [Fact]
        public void ItShouldAddTwoLengthsWhenTheyHaveDifferentMeasureUnits()
        {
            var tenInches = Length.OfInches(10f);
            var tewntyInches = Length.OfMillimeters(508f);

            Assert.Equal(Length.OfInches(30.0f), tenInches + tewntyInches);
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

            var sixtyInches = lengths.Sum();
            Assert.Equal(Length.OfInches(60.0f), sixtyInches);
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

            var sixtyInches = lengths.Sum();
            Assert.Equal(Length.OfMillimeters(60.0f), sixtyInches);
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

            var sixtyInches = lengths.Sum();
            Assert.Equal(Length.OfInches(60.0f), sixtyInches);
        }

        [Fact]
        public void ItShouldBePossibleToCompareTwoLengths()
        {
            var tenInches = Length.OfInches(10f);
            var tewntyInches = Length.OfInches(20f);

            Assert.True(tenInches < tewntyInches);
            Assert.True(tenInches <= tewntyInches);

            Assert.True(tewntyInches > tenInches);
            Assert.True(tewntyInches >= tenInches);
        }

        [Fact]
        public void ItShouldBePossibleToCompareTwoLengthsWithDifferentMeasureUnits()
        {
            var tenInches = Length.OfInches(10f);
            var twentyMillimeters = Length.OfMillimeters(20f);

            Assert.True(tenInches > twentyMillimeters);
            Assert.True(tenInches >= twentyMillimeters);

            Assert.True(twentyMillimeters < tenInches);
            Assert.True(twentyMillimeters <= tenInches);
        }
    }
}
