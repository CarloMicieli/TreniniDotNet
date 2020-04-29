using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlistItem
{
    public sealed class EditWishlistItemPresenter : DefaultHttpResultPresenter<EditWishlistItemOutput>, IEditWishlistItemOutputPort
    {
        public override void Standard(EditWishlistItemOutput output)
        {
            ViewModel = new OkObjectResult(new
            {
                Id = output.Id.ToGuid(),
                ItemId = output.ItemId.ToGuid()
            });
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
