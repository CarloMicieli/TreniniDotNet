using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.RemoveItemFromWishlist
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
