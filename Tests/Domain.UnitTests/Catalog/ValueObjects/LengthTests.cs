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

            zeroInches.Should().Be(Length.OfInches(0.0M));
            zeroMms.Should().Be(Length.OfMillimeters(0.0M));
        }

        [Fact]
        public void ItShouldThrowAnExceptionForNegativeLength()
        {
            Assert.Throws<ArgumentException>(() => Length.OfInches(-1.0M));
        }

        [Fact]
        public void ItShouldCreateAMillimetersLengthValues()
        {
            var l = Length.OfMillimeters(123.4M);
            l.ToMillimeters().Should().Be(123.4M);
        }

        [Fact]
        public void ItShouldCreateAInchesLengthValues()
        {
            var l = Length.OfInches(123.4M);
            l.ToInches().Should().Be(123.4M);
        }

        [Fact]
        public void ItShouldCheckLengthEquality()
        {
            var len1 = Length.OfInches(123.4M);
            var len2 = Length.OfInches(123.4M);

            (len1 == len2).Should().BeTrue();
            (len1.Equals(len2)).Should().BeTrue();
        }

        [Fact]
        public void ItShouldCheckLengthInequality()
        {
            var len1 = Length.OfInches(123.4M);
            var len2 = Length.OfInches(321.4M);

            (len1 != len2).Should().BeTrue();
            (len1.Equals(len2)).Should().BeFalse();
            (len1 == len2).Should().BeFalse();
        }

        [Fact]
        public void ItShouldProduceStringRepresentationsForLengthValues()
        {
            var len1 = Length.OfInches(123.4M);
            var len2 = Length.OfMillimeters(123.4M);

            var expectedIn = string.Format("{0}in", 123.4M);
            var expectedMm = string.Format("{0}mm", 123.4M);

            len1.ToString().Should().Be(expectedIn);
            len2.ToString().Should().Be(expectedMm);
        }

        [Fact]
        public void ItShouldAddTwoLengths()
        {
            var tenInches = Length.OfInches(10M);
            var tewntyInches = Length.OfInches(20M);

            (tenInches + tewntyInches).Should().Be(Length.OfInches(30.0M));
        }

        [Fact]
        public void ItShouldAddTwoLengthsWhenTheyHaveDifferentMeasureUnits()
        {
            var tenInches = Length.OfInches(10M);
            var tewntyInches = Length.OfMillimeters(508M);

            (tenInches + tewntyInches).Should().Be(Length.OfInches(30.0M));
        }

        [Fact]
        public void ItShouldSumMultipleLenghtsInInches()
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
        public void ItShouldSumMultipleLenghtsInMillimeters()
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
        public void ItShouldSumMultipleLenghtsWithDifferentMeasureUnits()
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
        public void ItShouldBePossibleToCompareTwoLengths()
        {
            var tenInches = Length.OfInches(10M);
            var tewntyInches = Length.OfInches(20M);

            (tenInches < tewntyInches).Should().BeTrue();
            (tenInches <= tewntyInches).Should().BeTrue();

            (tewntyInches > tenInches).Should().BeTrue();
            (tewntyInches >= tenInches).Should().BeTrue();
        }

        [Fact]
        public void ItShouldBePossibleToCompareTwoLengthsWithDifferentMeasureUnits()
        {
            var tenInches = Length.OfInches(10M);
            var twentyMillimeters = Length.OfMillimeters(20M);

            (tenInches > twentyMillimeters).Should().BeTrue();
            (tenInches >= twentyMillimeters).Should().BeTrue();

            (twentyMillimeters < tenInches).Should().BeTrue();
            (twentyMillimeters <= tenInches).Should().BeTrue();
        }

        [Fact]
        public void Length_ShouldCreateValue_WhenValuesAreCorrect()
        {
            var success = Length.TryCreate(200M, MeasureUnit.Millimeters, out var result);

            success.Should().BeTrue();
            result.Should().Be(Length.OfMillimeters(200M));
        }

        [Fact]
        public void Length_ShouldFailToCreateValue_WhenValuesAreNotCorrect()
        {
            var success = Length.TryCreate(-200M, MeasureUnit.Millimeters, out var result);
            success.Should().BeFalse();
        }

        [Fact]
        public void Length_ShouldCreateNullValue_WhenValueIsNull()
        {
            var success = Length.TryCreate(null, MeasureUnit.Millimeters, out var result);
            success.Should().BeTrue();
            result.HasValue.Should().BeFalse();
        }

        [Fact]
        public void Length_NullValue_ShouldThrowNullReferenceExceptionFromItsMethods()
        {
            var _ = Length.TryCreate(null, MeasureUnit.Millimeters, out var nullLength);

            nullLength.Invoking(it => it.ToString())
                .Should().Throw<NullReferenceException>()
                .WithMessage("Object reference not set to an instance of an object.");

            nullLength.Invoking(it => it.ToMillimeters())
                .Should().Throw<NullReferenceException>()
                .WithMessage("Object reference not set to an instance of an object.");

            nullLength.Invoking(it => it.ToInches())
                .Should().Throw<NullReferenceException>()
                .WithMessage("Object reference not set to an instance of an object.");
        }
    }
}
