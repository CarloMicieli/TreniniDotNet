using System;
using FluentAssertions;
using TreniniDotNet.Common.Lengths;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class MinRadiusTests
    {
        [Fact]
        public void MinRadius_ShouldCreateValues_FromMillimeterValues()
        {
            var minRadius = MinRadius.OfMillimeters(360M);
            minRadius.Should().NotBeNull();
            minRadius.Value.Should().Be(Length.OfMillimeters(360));
        }

        [Fact]
        public void MinRadius_ShouldProduceStringRepresentations()
        {
            var minRadius = MinRadius.OfMillimeters(360M);
            minRadius.ToString().Should().Be("360 mm");
        }

        [Fact]
        public void MinRadius_ShouldThrowArgumentOutOfRangeException_WhenValueIsNegative()
        {
            Action act = () => MinRadius.OfMillimeters(-100);
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("Invalid value for a length, it must be positive. (Parameter 'value')");
        }

        [Fact]
        public void MinRadius_ShouldCheckForEquality()
        {
            var min1 = MinRadius.OfMillimeters(360);
            var min2 = MinRadius.OfMillimeters(360);
            var min3 = MinRadius.OfMillimeters(630);

            min1.Equals(min2).Should().BeTrue();
            min1.Equals(min3).Should().BeFalse();
            (min1 == min2).Should().BeTrue();
            (min1 != min2).Should().BeFalse();
            (min1 == min3).Should().BeFalse();
            (min1 != min3).Should().BeTrue();
        }
    }
}
