using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.DeleteWishlist
{
    public sealed class DeleteWishlistPresenter : DefaultHttpResultPresenter<DeleteWishlistOutput>, IDeleteWishlistOutputPort
    {
        public override void Standard(DeleteWishlistOutput output)
        {
            ViewModel = new NoContentResult();
        }

        public void WishlistNotFound(WishlistId id)
        {
            ViewModel = new NotFoundObjectResult(new { Id = id.ToGuid() });
        }
    }
}
