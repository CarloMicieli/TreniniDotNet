using TreniniDotNet.Application.Collecting.Wishlists.EditWishlist;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlist
{
    public sealed class EditWishlistPresenter : DefaultHttpResultPresenter<EditWishlistOutput>, IEditWishlistOutputPort
    {
        public override void Standard(EditWishlistOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void WishlistNotFound(WishlistId id)
        {
            throw new System.NotImplementedException();
        }

        public void NotAuthorizedToEditThisWishlist(Owner owner)
        {
            throw new System.NotImplementedException();
        }
    }
}
