using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Shops.RemoveShopFromFavourites
{
    public sealed class RemoveShopFromFavouritesPresenter : DefaultHttpResultPresenter<RemoveShopFromFavouritesOutput>, IRemoveShopFromFavouritesOutputPort
    {
        public override void Standard(RemoveShopFromFavouritesOutput output)
        {
            ViewModel = new OkResult();
        }

        public void ShopNotFound(ShopId shopId)
        {
            ViewModel = new NotFoundObjectResult(new { ShopId = shopId });
        }
    }
}
