using TreniniDotNet.Application.Boundaries.Catalog.GetCatalogItemBySlug;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugPresenter : DefaultHttpResultPresenter<GetCatalogItemBySlugOutput>, IGetCatalogItemBySlugOutputPort
    {
        public void CatalogItemNotFound(string message)
        {
            throw new System.NotImplementedException();
        }

        public override void Standard(GetCatalogItemBySlugOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}