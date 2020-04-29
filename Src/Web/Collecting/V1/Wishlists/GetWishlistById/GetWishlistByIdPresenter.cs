using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistById
{
    public sealed class GetWishlistByIdPresenter : DefaultHttpResultPresenter<GetWishlistByIdOutput>, IGetWishlistByIdOutputPort
    {
        public override void Standard(GetWishlistByIdOutput output)
        {
            ViewModel = new OkObjectResult(new WishlistView(output.Wishlist));
        }

        public void WishlistNotFound(WishlistId id)
        {
            ViewModel = new NotFoundObjectResult(new { Id = id });
        }

        public void WishlistNotVisible(WishlistId id, Visibility visibility)
        {
            ViewModel = new NotFoundObjectResult(new { Id = id });
        }
    }
}
