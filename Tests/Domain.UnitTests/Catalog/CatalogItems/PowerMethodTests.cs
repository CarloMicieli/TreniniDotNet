using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class PowerMethodTests
    {
        [Fact]
        public void PowerMethod_shouldBeConvertedFromStrings()
        {
            Assert.Equal(
                PowerMethod.AC,
                PowerMethod.AC.ToString().ToPowerMethod());
        }

        [Fact]
        public void PowerMethod_shouldBeFailToConvertedInvalidStrings()
        {
            Assert.Null("not-valid".ToPowerMethod());
        }

        [Fact]
        public void PowerMethod_ShouldParseStringToPowerMethods()
        {
            bool success = PowerMethods.TryParse("dc", out var powerMethod);

            success.Should().BeTrue();
            powerMethod.Should().Be(PowerMethod.DC);
        }

        [Fact]
        public void PowerMethod_ShouldFailToParseStringToPowerMethods_WhenStringIsInvalid()
        {
            bool success = PowerMethods.TryParse("   invalid", out var powerMethod);

            success.Should().BeFalse();
        }

    }
}