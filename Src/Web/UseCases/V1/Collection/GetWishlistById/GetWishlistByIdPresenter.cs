using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistById;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.V1.Collection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistById
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
