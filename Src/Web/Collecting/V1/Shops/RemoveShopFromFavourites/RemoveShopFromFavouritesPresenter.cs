using TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Shops.RemoveShopFromFavourites
{
    public sealed class RemoveShopFromFavouritesPresenter : DefaultHttpResultPresenter<RemoveShopFromFavouritesOutput>, IRemoveShopFromFavouritesOutputPort
    {
        public override void Standard(RemoveShopFromFavouritesOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void ShopNotFound(ShopId shopId)
        {
            throw new System.NotImplementedException();
        }
    }
}
