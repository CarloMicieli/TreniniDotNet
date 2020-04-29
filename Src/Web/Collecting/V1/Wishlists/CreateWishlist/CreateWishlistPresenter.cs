using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist;
using TreniniDotNet.Common;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistPresenter : DefaultHttpResultPresenter<CreateWishlistOutput>, ICreateWishlistOutputPort
    {
        public override void Standard(CreateWishlistOutput output)
        {
            ViewModel = Created(
                nameof(GetWishlistById.WishlistsController.GetWishlistById),
                new
                {
                    id = output.WishlistId,
                    version = "1",
                },
                output);
        }

        public void WishlistAlreadyExists(Slug wishlistSlug)
        {
            ViewModel = new ConflictObjectResult(new { Slug = wishlistSlug });
        }
    }
}
