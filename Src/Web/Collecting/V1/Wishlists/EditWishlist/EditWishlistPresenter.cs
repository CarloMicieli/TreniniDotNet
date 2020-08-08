using Microsoft.AspNetCore.Mvc;
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
            ViewModel = new OkResult();
        }

        public void WishlistNotFound(WishlistId id)
        {
            ViewModel = new NotFoundResult();
        }

        public void NotAuthorizedToEditThisWishlist(Owner owner)
        {
            ViewModel = new NotFoundResult();
        }
    }
}
