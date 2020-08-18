using TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist;

namespace TreniniDotNet.Application.Collecting.Wishlists
{
    public static class NewCreateWishlistInput
    {
        public static readonly CreateWishlistInput Empty = With();

        public static CreateWishlistInput With(string owner = null, string listName = null, string visibility = null, BudgetInput budget = null) =>
            new CreateWishlistInput(owner, listName, visibility, budget);
    }
}
