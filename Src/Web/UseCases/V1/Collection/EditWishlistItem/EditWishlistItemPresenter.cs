using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.EditWishlistItem
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
