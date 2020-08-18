using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetFavouriteShops
{
    public sealed class GetFavouriteShopsPresenter : DefaultHttpResultPresenter<GetFavouriteShopsOutput>, IGetFavouriteShopsOutputPort
    {
        public override void Standard(GetFavouriteShopsOutput output)
        {
            ViewModel = new OkResult();
        }
    }
}
