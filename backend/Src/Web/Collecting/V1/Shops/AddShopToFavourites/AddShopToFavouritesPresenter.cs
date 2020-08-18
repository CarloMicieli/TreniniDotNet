using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Shops.AddShopToFavourites
{
    public sealed class AddShopToFavouritesPresenter : DefaultHttpResultPresenter<AddShopToFavouritesOutput>, IAddShopToFavouritesOutputPort
    {
        public override void Standard(AddShopToFavouritesOutput output)
        {
            ViewModel = new OkResult();
        }

        public void ShopNotFound(ShopId shopId)
        {
            ViewModel = new NotFoundObjectResult(new { ShopId = shopId });
        }
    }
}
