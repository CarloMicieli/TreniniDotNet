using TreniniDotNet.Application.Boundaries.Collection.RemoveShopFromFavourites;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.RemoveShopFromFavourites
{
    public sealed class RemoveShopFromFavouritesPresenter : DefaultHttpResultPresenter<RemoveShopFromFavouritesOutput>, IRemoveShopFromFavouritesOutputPort
    {
        public override void Standard(RemoveShopFromFavouritesOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
