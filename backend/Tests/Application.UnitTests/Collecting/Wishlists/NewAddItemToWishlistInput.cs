using System;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist;

namespace TreniniDotNet.Application.Collecting.Wishlists
{
    public static class NewAddItemToWishlistInput
    {
        public static readonly AddItemToWishlistInput Empty = With();

        public static AddItemToWishlistInput With(
            string owner = null,
            Guid? id = null,
            string catalogItem = null,
            PriceInput price = null,
            DateTime? addedDate = null,
            string priority = null,
            string notes = null) =>
            new AddItemToWishlistInput(
                owner,
                id ?? Guid.Empty,
                catalogItem,
                addedDate ?? DateTime.Now,
                price,
                priority,
                notes);
    }
}
