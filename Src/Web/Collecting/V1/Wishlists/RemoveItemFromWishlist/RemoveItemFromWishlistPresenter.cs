using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistPresenter : DefaultHttpResultPresenter<RemoveItemFromWishlistOutput>, IRemoveItemFromWishlistOutputPort
    {
        public override void Standard(RemoveItemFromWishlistOutput output)
        {
            ViewModel = new NoContentResult();
        }

        public void WishlistItemNotFound(WishlistId id, WishlistItemId itemId)
        {
            ViewModel = new NotFoundObjectResult(new
            {
                Id = id.ToGuid(),
                ItemId = itemId.ToGuid()
            });
        }
    }
}
