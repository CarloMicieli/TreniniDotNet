using System;
using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class EpochTests
    {
        [Fact]
        public void Epoch_Constant_ShouldProvideTheMainEpochs()
        {
            Epoch.I.ToString().Should().Be("I");
            Epoch.II.ToString().Should().Be("II");
            Epoch.III.ToString().Should().Be("III");
            Epoch.IIIa.ToString().Should().Be("IIIa");
            Epoch.IIIb.ToString().Should().Be("IIIb");
            Epoch.IV.ToString().Should().Be("IV");
            Epoch.V.ToString().Should().Be("V");
            Epoch.VI.ToString().Should().Be("VI");
        }

        [Fact]
        public void Epoch_TryParse_ShouldParseValidEpochValues()
        {
            var result = Epoch.TryParse("III", out var epoch);

            result.Should().BeTrue();
            epoch.Should().Be(Epoch.III);
        }

        [Fact]
        public void Epoch_TryParse_ShouldParseValidMultipleEpochValues()
        {
            var expected = "III/IV";
            var result = Epoch.TryParse(expected, out var epoch);

            result.Should().BeTrue();
            epoch?.ToString().Should().Be(expected);
        }

        [Fact]
        public void Epoch_TryParse_ShouldFailToParseInvalidValues()
        {
            var result = Epoch.TryParse("Invalid", out _);
            result.Should().BeFalse();

            var result2 = Epoch.TryParse("III/IV/V", out _);
            result2.Should().BeFalse();
        }

        [Fact]
        public void Epoch_Parse_ShouldParseValidEpochs()
        {
            var epoch = Epoch.Parse("III");
            epoch.Should().Be(Epoch.III);
        }

        [Fact]
        public void Epoch_Parse_ShouldFailToParseInvalidEpochs()
        {
            Action act = () => Epoch.Parse("invalid");

            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("The value is not a valid Epoch (Parameter 'str')");
        }
    }
}
