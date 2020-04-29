using TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Shops.AddShopToFavourites
{
    public sealed class AddShopToFavouritesPresenter : DefaultHttpResultPresenter<AddShopToFavouritesOutput>, IAddShopToFavouritesOutputPort
    {
        public override void Standard(AddShopToFavouritesOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
