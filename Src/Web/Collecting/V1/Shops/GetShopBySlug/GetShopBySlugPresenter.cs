using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Shops.GetShopBySlug;
using TreniniDotNet.Common;
using TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetShopBySlug
{
    public class GetShopBySlugPresenter : DefaultHttpResultPresenter<GetShopBySlugOutput>, IGetShopBySlugOutputPort
    {
        public void ShopNotFound(Slug slug)
        {
            ViewModel = new NotFoundObjectResult(new { Slug = slug.Value });
        }

        public override void Standard(GetShopBySlugOutput output)
        {
            ViewModel = new OkObjectResult(new ShopView(output.Shop));
        }
    }
}