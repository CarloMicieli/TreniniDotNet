using System;
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlist;

namespace TreniniDotNet.Application.Collecting.Wishlists
{
    public static class NewEditWishlistInput
    {
        public static readonly EditWishlistInput Empty = With();

        public static EditWishlistInput With(Guid? id = null,
            string owner = null,
            string listName = null,
            string visibility = null,
            BudgetInput budget = null) =>
            new EditWishlistInput(id ?? Guid.Empty, owner, listName, visibility, budget);
    }
}
