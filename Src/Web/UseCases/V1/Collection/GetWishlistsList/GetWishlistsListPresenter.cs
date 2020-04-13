using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsList;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistsList
{
    public sealed class GetWishlistsListPresenter : DefaultHttpResultPresenter<GetWishlistsListOutput>, IGetWishlistsListOutputPort
    {
        public override void Standard(GetWishlistsListOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
