using TreniniDotNet.Application.Boundaries.Catalog.EditBrand;
using TreniniDotNet.Common;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditBrand
{
    public sealed class EditBrandPresenter : DefaultHttpResultPresenter<EditBrandOutput>, IEditBrandOutputPort
    {
        public void BrandNotFound(Slug brandSlug)
        {
            throw new System.NotImplementedException();
        }

        public override void Standard(EditBrandOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
