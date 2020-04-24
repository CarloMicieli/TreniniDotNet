using TreniniDotNet.Application.Boundaries.Collection.AddShopToFavourites;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddShopToFavourites
{
    public sealed class AddShopToFavouritesPresenter : DefaultHttpResultPresenter<AddShopToFavouritesOutput>, IAddShopToFavouritesOutputPort
    {
        public override void Standard(AddShopToFavouritesOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
