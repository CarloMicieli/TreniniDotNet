using FluentAssertions;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public class WishlistSummaryTests
    {
        [Fact]
        public void WishlistSummary_ShouldCreateAnEmptySummary()
        {
            var wishlist = CollectingSeedData.Wishlists.New()
                .Owner(new Owner("George"))
                .Budget(new Budget(1000M, "EUR"))
                .Build();

            var summary = WishlistSummary.Of(wishlist);

            summary.Should().NotBeNull();
            summary.Currency.Should().Be("EUR");
            summary.Budget.Should().Be(1000M);
            summary.Allocated.Should().Be(0M);
            summary.Percentage.Should().Be(0);
        }

        [Fact]
        public void WishlistSummary_ShouldCreateAWishlistSummary()
        {
            var wishlist = CollectingSeedData.Wishlists.New()
                .Owner(new Owner("George"))
                .Budget(new Budget(1000M, "EUR"))
                .Item(ib => ib
                    .Price(Price.Euro(150))
                    .Build())
                .Build();

            var summary = WishlistSummary.Of(wishlist);

            summary.Should().NotBeNull();
            summary.Currency.Should().Be("EUR");
            summary.Budget.Should().Be(1000M);
            summary.Allocated.Should().Be(150M);
            summary.Percentage.Should().Be(15);
        }
    }
}
