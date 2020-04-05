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
    }
}