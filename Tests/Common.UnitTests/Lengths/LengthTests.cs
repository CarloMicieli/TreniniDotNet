using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Common.Lengths
{
    public class LengthTests
    {
        private readonly Length tenInches;
        private readonly Length twentyMillimeters;
        private readonly Length tewntyInches;
        private readonly Length anotherTenInches;

        public LengthTests()
        {
            tenInches = Length.OfInches(10M);
            twentyMillimeters = Length.OfMillimeters(20M);
            tewntyInches = Length.OfInches(20M);
            anotherTenInches = Length.OfInches(10M);
        }

        [Fact]
        public void Length_OfMillimeters_ShouldCreateLengths()
        {
            var len = Length.OfMillimeters(210.0M);

            len.Should().NotBeNull();
            len.MeasureUnit.Should().Be(MeasureUnit.Millimeters);
            len.Value.Should().Be(210.0M);
        }

        [Fact]
        public void Length_OfInches_ShouldCreateLengths()
        {
            var len = Length.OfInches(210.0M);

            len.Should().NotBeNull();
            len.MeasureUnit.Should().Be(MeasureUnit.Inches);
            len.Value.Should().Be(210.0M);
        }

        [Fact]
        public void Length_OfMillimeters_ShouldThrowAnExceptionForNegativeLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Length.OfMillimeters(-1.0M));
        }

        [Fact]
        public void Length_OfInches_ShouldThrowAnExceptionForNegativeLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Length.OfInches(-1.0M));
        }

        [Fact]
        public void Length_Compare_ShouldCompareTwoLengthsWithSameMeasureUnit()
        {
            (tenInches < tewntyInches).Should().BeTrue();
            (tenInches <= tewntyInches).Should().BeTrue();

            (tewntyInches > tenInches).Should().BeTrue();
            (tewntyInches >= tenInches).Should().BeTrue();
        }

        [Fact]
        public void Length_Compare_ShouldCompareLengthsWithDifferentMeasureUnits()
        {
            (tenInches > twentyMillimeters).Should().BeTrue();
            (tenInches >= twentyMillimeters).Should().BeTrue();

            (twentyMillimeters < tenInches).Should().BeTrue();
            (twentyMillimeters <= tenInches).Should().BeTrue();
        }

        [Fact]
        public void Length_Deconstruct_ShouldExtractValues()
        {
            var (value, mu) = tenInches;
            value.Should().Be(tenInches.Value);
            mu.Should().Be(tenInches.MeasureUnit);
        }

        [Fact]
        public void Length_Equals_ShouldCheckLengthEquality()
        {
            (tenInches == anotherTenInches).Should().BeTrue();
            (tenInches.Equals(anotherTenInches)).Should().BeTrue();
        }

        [Fact]
        public void Length_Equals_ShouldCheckLengthInequality()
        {
            (tenInches != twentyMillimeters).Should().BeTrue();
            (tenInches.Equals(twentyMillimeters)).Should().BeFalse();
            (tenInches == twentyMillimeters).Should().BeFalse();
        }

        [Fact]
        public void Length_ToString_ShouldProduceStringRepresentations()
        {
            var expectedIn = string.Format("{0} in", tenInches.Value);
            var expectedMm = string.Format("{0} mm", twentyMillimeters.Value);

            tenInches.ToString().Should().Be(expectedIn);
            twentyMillimeters.ToString().Should().Be(expectedMm);
        }

        [Fact]
        public void Length_Add_ShouldAddTwoLengths()
        {
            var tenInches = Length.OfInches(10M);
            var tewntyInches = Length.OfInches(20M);

            (tenInches + tewntyInches).Should().Be(Length.OfInches(30.0M));
            tenInches.Add(tewntyInches).Should().Be(Length.OfInches(30.0M));
        }

        [Fact]
        public void Length_Add_ShouldAddTwoLengthsWhenTheyHaveDifferentMeasureUnits()
        {
            var tenInches = Length.OfInches(10M);
            var tewntyInches = Length.OfMillimeters(508M);

            (tenInches + tewntyInches).Should().Be(Length.OfInches(30.0M));
            tenInches.Add(tewntyInches).Should().Be(Length.OfInches(30.0M));
        }

        [Fact]
        public void Length_EnumerableSum_ShouldSumMultipleLenghtsInInches()
        {
            var lengths = new List<Length>()
            {
                Length.OfInches(10M),
                Length.OfInches(20M),
                Length.OfInches(30M)
            };

            lengths.Sum().Should().Be(Length.OfInches(60.0M));
        }

        [Fact]
        public void Length_EnumerableSum_ShouldSumMultipleLenghtsInMillimeters()
        {
            var lengths = new List<Length>()
            {
                Length.OfMillimeters(10M),
                Length.OfMillimeters(20M),
                Length.OfMillimeters(30M)
            };

            lengths.Sum().Should().Be(Length.OfMillimeters(60.0M));
        }

        [Fact]
        public void Length_EnumerableSum_ShouldSumMultipleLenghtsWithDifferentMeasureUnits()
        {
            var lengths = new List<Length>()
            {
                Length.OfInches(10M),
                Length.OfMillimeters(508M),
                Length.OfInches(30M)
            };

            lengths.Sum().Should().Be(Length.OfInches(60.0M));
        }

        [Fact]
        public void Length_TryCreate_ShouldCreateValue_WhenValuesAreCorrect()
        {
            var result = Length.TryCreate(200M, MeasureUnit.Millimeters, out var length);
            result.Should().BeTrue();
            length.Should().Be(Length.OfMillimeters(200M));
        }

        [Fact]
        public void Length_TryCreate_ShouldFailToCreateValue_WhenValuesAreNotCorrect()
        {
            var result = Length.TryCreate(-200M, MeasureUnit.Millimeters, out var length);
            result.Should().BeFalse();
        }

        [Fact]
        public void Length_TryCreate_ShouldCreateNullValue_WhenValueIsNull()
        {
            var success = Length.TryCreate(null, MeasureUnit.Millimeters, out var length);
            success.Should().BeFalse();
        }
    }
}