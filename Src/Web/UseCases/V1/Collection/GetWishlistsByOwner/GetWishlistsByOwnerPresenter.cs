using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.V1.Collection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerPresenter : DefaultHttpResultPresenter<GetWishlistsByOwnerOutput>, IGetWishlistsByOwnerOutputPort
    {
        public override void Standard(GetWishlistsByOwnerOutput output)
        {
            ViewModel = new OkObjectResult(new WishlistsView(output.Owner, output.Visibility, output.Wishlists));
        }

        public void WishlistsNotFoundForTheOwner(Owner owner, VisibilityCriteria visibility)
        {
            ViewModel = new NotFoundObjectResult(new
            {
                Owner = owner.Value,
                Visibility = visibility.ToString()
            });
        }
    }
}
