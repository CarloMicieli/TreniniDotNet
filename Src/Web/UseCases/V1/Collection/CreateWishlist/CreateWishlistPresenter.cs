using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.CreateWishlist;
using TreniniDotNet.Common;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateWishlist
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
