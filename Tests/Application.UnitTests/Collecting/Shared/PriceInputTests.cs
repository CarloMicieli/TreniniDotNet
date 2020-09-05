using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shared
{
    public class PriceInputTests
    {
        [Fact]
        public void PriceInput_ToPrice_ShouldCreatePriceValues()
        {
            var priceInput = NewPriceInput.With(150M, "EUR");

            var price = priceInput.ToPriceOrDefault();

            price.Should().NotBeNull();
            price?.Amount.Should().Be(150M);
            price?.Currency.Should().Be("EUR");
        }

        [Fact]
        public void PriceInput_ToPrice_ShouldReturnNullWhenInvalid()
        {
            var priceInput = NewPriceInput.With(0M, "");

            var price = priceInput.ToPriceOrDefault();

            price.Should().BeNull();
        }
    }
}
