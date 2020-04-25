using TreniniDotNet.Application.Boundaries.Catalog.EditCatalogItem;
using TreniniDotNet.Common;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditCatalogItem
{
    public sealed class EditCatalogItemPresenter : DefaultHttpResultPresenter<EditCatalogItemOutput>, IEditCatalogItemOutputPort
    {
        public void CatalogItemNotFound(Slug slug)
        {
            throw new System.NotImplementedException();
        }

        public override void Standard(EditCatalogItemOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
