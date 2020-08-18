using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public class BudgetTests
    {
        [Fact]
        public void Budget_ShouldCreateNewValues()
        {
            var budget = new Budget(1000M, "EUR");
            budget.Amount.Should().Be(1000M);
            budget.Currency.Should().Be("EUR");
        }

        [Fact]
        public void Budget_ShouldThrowExceptionWhenValueIsNegative()
        {
            Action action = () =>
            {
                var budget = new Budget(-1000M, "EUR");
            };

            action.Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Budget_ShouldCheckForEquality()
        {
            var fiftyEuro1 = new Budget(50M, "EUR");
            var fiftyEuro2 = new Budget(50M, "EUR");
            var fiftyPounds = new Budget(50M, "GBP");

            (fiftyEuro1 == fiftyEuro2).Should().BeTrue();
            (fiftyEuro1 != fiftyEuro2).Should().BeFalse();
            fiftyEuro1.Equals(fiftyEuro2).Should().BeTrue();

            fiftyEuro1.Equals(fiftyPounds).Should().BeFalse();
            (fiftyEuro1 == fiftyPounds).Should().BeFalse();
            (fiftyEuro1 != fiftyPounds).Should().BeTrue();
        }
    }
}
