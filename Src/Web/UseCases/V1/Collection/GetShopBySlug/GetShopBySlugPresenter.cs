using TreniniDotNet.Application.Boundaries.Collection.GetShopBySlug;
using TreniniDotNet.Common;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetShopBySlug
{
    public class GetShopBySlugPresenter : DefaultHttpResultPresenter<GetShopBySlugOutput>, IGetShopBySlugOutputPort
    {
        public void ShopNotFound(Slug slug)
        {
            throw new System.NotImplementedException();
        }

        public override void Standard(GetShopBySlugOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}