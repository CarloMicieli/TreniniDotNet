using TreniniDotNet.Application.Boundaries.Collection.GetFavouriteShops;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetFavouriteShops
{
    public sealed class GetFavouriteShopsPresenter : DefaultHttpResultPresenter<GetFavouriteShopsOutput>, IGetFavouriteShopsOutputPort
    {
        public override void Standard(GetFavouriteShopsOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
