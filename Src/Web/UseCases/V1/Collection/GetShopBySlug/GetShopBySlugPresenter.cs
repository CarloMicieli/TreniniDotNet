using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.GetShopBySlug;
using TreniniDotNet.Common;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.V1.Collection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetShopBySlug
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