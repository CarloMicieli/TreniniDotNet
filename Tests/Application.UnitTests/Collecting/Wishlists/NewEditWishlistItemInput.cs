using System;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem;

namespace TreniniDotNet.Application.Collecting.Wishlists
{
    public static class NewEditWishlistItemInput
    {
        public static readonly EditWishlistItemInput Empty = With();

        public static EditWishlistItemInput With(
            string owner = null,
            Guid? id = null,
            Guid? itemId = null,
            DateTime? addedDate = null,
            PriceInput price = null,
            string priority = null,
            string notes = null) => new EditWishlistItemInput(
            owner,
            id ?? Guid.Empty,
            itemId ?? Guid.Empty,
            addedDate,
            price,
            priority,
            notes);
    }
}
