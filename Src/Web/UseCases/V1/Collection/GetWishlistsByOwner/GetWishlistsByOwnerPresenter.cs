using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerPresenter : DefaultHttpResultPresenter<GetWishlistsByOwnerOutput>, IGetWishlistsByOwnerOutputPort
    {
        public override void Standard(GetWishlistsByOwnerOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void WishlistsNotFoundForTheOwner(Owner owner, Visibility visibility)
        {
            throw new System.NotImplementedException();
        }
    }
}
