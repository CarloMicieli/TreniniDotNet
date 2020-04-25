using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.EditBrand;
using TreniniDotNet.Common;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditBrand
{
    public sealed class EditBrandPresenter : DefaultHttpResultPresenter<EditBrandOutput>, IEditBrandOutputPort
    {
        public void BrandNotFound(Slug brandSlug)
        {
            ViewModel = new NotFoundObjectResult(new { Slug = brandSlug });
        }

        public override void Standard(EditBrandOutput output)
        {
            ViewModel = new OkResult();
        }
    }
}
