using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    public class PriceTests
    {
        [Fact]
        public void Price_ShouldCreateNewValues()
        {
            var price = new Price(150M, "USD");

            price.Should().NotBeNull();
            price.Amount.Should().Be(150M);
            price.Currency.Should().Be("USD");
        }

        [Fact]
        public void Price_TryCreate_ShouldCreateValuesForValidInputs()
        {
            var isOk = Price.TryCreate(100M, "USD", out var price);

            isOk.Should().BeTrue();
            price.Amount.Should().Be(100M);
            price.Currency.Should().Be("USD");
        }

        [Fact]
        public void Price_TryCreate_ShouldFailToCreateValuesForInvalidInputs()
        {
            Price.TryCreate(-100M, "USD", out var _)
                .Should().BeFalse();

            Price.TryCreate(100M, "--", out var _)
                .Should().BeFalse();
        }

        [Fact]
        public void Price_ShouldThrowException_ForInvalidCurrencyCode()
        {
            Action act = () => new Price(10M, "---");
            act.Should().Throw<Exception>()
                .WithMessage("--- is an unknown currency code!");
        }

        [Fact]
        public void Price_ShouldThrowException_ForNegativeAmounts()
        {
            Action act = () => new Price(-10M, "USD");
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Specified argument was out of the range of valid values. (Parameter 'amount')");
        }

        [Fact]
        public void Price_ShouldCreateNewValues_FromSpecificCurrency()
        {
            var fiftyEuro = Price.Euro(50M);
            var fiftyPounds = Price.Pounds(50M);
            var fiftyDollars = Price.Dollars(50M);

            fiftyEuro.Amount.Should().Be(50M);
            fiftyEuro.Currency.Should().Be("EUR");

            fiftyPounds.Amount.Should().Be(50M);
            fiftyPounds.Currency.Should().Be("GBP");

            fiftyDollars.Amount.Should().Be(50M);
            fiftyDollars.Currency.Should().Be("USD");
        }

        [Fact]
        public void Price_ShouldCheckForEquality()
        {
            var fiftyEuro1 = Price.Euro(50M);
            var fiftyEuro2 = Price.Euro(50M);
            var fiftyPounds = Price.Pounds(50M);

            (fiftyEuro1 == fiftyEuro2).Should().BeTrue();
            (fiftyEuro1 != fiftyEuro2).Should().BeFalse();
            fiftyEuro1.Equals(fiftyEuro2).Should().BeTrue();

            fiftyEuro1.Equals(fiftyPounds).Should().BeFalse();
            (fiftyEuro1 == fiftyPounds).Should().BeFalse();
            (fiftyEuro1 != fiftyPounds).Should().BeTrue();
        }
    }
}
