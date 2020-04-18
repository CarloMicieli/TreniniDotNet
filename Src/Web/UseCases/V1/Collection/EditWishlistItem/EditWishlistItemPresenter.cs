using TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.EditWishlistItem
{
    public sealed class EditWishlistItemPresenter : DefaultHttpResultPresenter<EditWishlistItemOutput>, IEditWishlistItemOutputPort
    {
        public override void Standard(EditWishlistItemOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void WishlistItemNotFound(WishlistId id, WishlistItemId itemId)
        {
            throw new System.NotImplementedException();
        }
    }
}
