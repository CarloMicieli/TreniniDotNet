using System;
using TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist;

namespace TreniniDotNet.Application.Collecting.Wishlists
{
    public static class NewDeleteWishlistInput
    {
        public static readonly DeleteWishlistInput Empty = With();

        public static DeleteWishlistInput With(Guid? id = null, string owner = null) =>
            new DeleteWishlistInput(owner, id ?? Guid.Empty);
    }
}