using Xunit;
using FluentAssertions;
using TreniniDotNet.Common.Lengths;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public class RailwayLengthTests
    {
        [Fact]
        public void RailwayLength_Create_ShouldCreateNewRailwayLengths()
        {
            var railwayLength = RailwayLength.Create(100M, 90M);

            railwayLength.Should().NotBeNull();
            railwayLength.Kilometers.Should().Be(Length.Of(100M, MeasureUnit.Kilometers));
            railwayLength.Miles.Should().Be(Length.Of(90M, MeasureUnit.Miles));
        }

        [Fact]
        public void RailwayLength_TryCreate_ShouldCreateNewRailwayLengths()
        {
            var result = RailwayLength.TryCreate(100M, 90M, out var railwayLength);

            result.Should().BeTrue();
            railwayLength.Should().NotBeNull();
            railwayLength.Kilometers.Should().Be(Length.Of(100M, MeasureUnit.Kilometers));
            railwayLength.Miles.Should().Be(Length.Of(90M, MeasureUnit.Miles));
        }

        [Fact]
        public void RailwayLength_TryCreate_ShouldReturnFalseForNullValues()
        {
            var result = RailwayLength.TryCreate(null, null, out var railwayLength);

            result.Should().BeFalse();
            railwayLength.Should().BeNull();
        }

        [Fact]
        public void RailwayLength_TryCreate_ShouldReturnFalseForNegativeValues()
        {
            var result = RailwayLength.TryCreate(-1M, -1M, out var railwayLength);

            result.Should().BeFalse();
            railwayLength.Should().BeNull();
        }
    }
}