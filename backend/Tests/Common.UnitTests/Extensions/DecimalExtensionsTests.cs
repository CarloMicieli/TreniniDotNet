using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Common.Extensions
{
    public class DecimalExtensionsTests
    {
        [Fact]
        public void DecimalExtensions_IsPositive_ShouldCheckForPositiveNumbers()
        {
            decimal fortyTwo = 42M;
            decimal minusFortyTwo = -42M;

            fortyTwo.IsPositive().Should().BeTrue();
            minusFortyTwo.IsPositive().Should().BeFalse();
        }

        [Fact]
        public void DecimalExtensions_IsNegative_ShouldCheckForNegativeNumbers()
        {
            decimal fortyTwo = 42M;
            decimal minusFortyTwo = -42M;

            fortyTwo.IsNegative().Should().BeFalse();
            minusFortyTwo.IsNegative().Should().BeTrue();
        }
    }
}
