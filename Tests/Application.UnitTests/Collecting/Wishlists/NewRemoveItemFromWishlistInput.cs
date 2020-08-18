using System;
using TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist;

namespace TreniniDotNet.Application.Collecting.Wishlists
{
    public static class NewRemoveItemFromWishlistInput
    {
        public static readonly RemoveItemFromWishlistInput Empty = With();

        public static RemoveItemFromWishlistInput With(
            string owner = null,
            Guid? id = null,
            Guid? itemId = null) => new RemoveItemFromWishlistInput(owner, id ?? Guid.Empty, itemId ?? Guid.Empty);
    }
}