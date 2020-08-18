using System;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public sealed class WishlistSummary
    {
        private WishlistSummary(string? currency, decimal? budget, decimal allocated, int percentage)
        {
            Currency = currency;
            Budget = budget;
            Allocated = allocated;
            Percentage = percentage;
        }

        public static WishlistSummary Of(Wishlist wishlist)
        {
            var (allocated, _) = wishlist.CalculateTotalValue();
            var totalBudget = (wishlist.Budget?.Amount == 0) ? 0.0M : wishlist.Budget!.Amount;
            var percentage = (int)(Math.Round(allocated / totalBudget, 2) * 100M);

            return new WishlistSummary(wishlist.Budget?.Currency, wishlist.Budget?.Amount, allocated, percentage);
        }

        public string? Currency { get; }
        public decimal? Budget { get; }
        public decimal Allocated { get; }
        public int Percentage { get; }
    }
}
