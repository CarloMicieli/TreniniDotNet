using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistsByOwner
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
