using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class EraTests
    {
        [Fact]
        public void Era_ShouldBeConvertedFromStrings()
        {
            var era = Era.III.ToString().ToEra();
            era.Should().Be(Era.III);
        }

        [Fact]
        public void Era_ShouldBeFailToConvertedInvalidStrings()
        {
            var invalid = "not-valid".ToEra();
            invalid.Should().BeNull();
        }

        [Fact]
        public void Eras_ShouldCreateValue_FromValidStrings()
        {
            bool success = Eras.TryParse(Era.III.ToString(), out var era);

            success.Should().BeTrue();
            era.Should().Be(Era.III);
        }

        [Fact]
        public void Eras_ShouldCreateValue_FromValidStringsIgnoringCase()
        {
            bool success = Eras.TryParse(Era.III.ToString().ToLower(), out var era);

            success.Should().BeTrue();
            era.Should().Be(Era.III);
        }

        [Fact]
        public void Eras_ShouldFailToCreateValues_FromInvalidStrings()
        {
            bool fail = Eras.TryParse(" invalid", out var era);
            fail.Should().BeFalse();
        }
    }
}