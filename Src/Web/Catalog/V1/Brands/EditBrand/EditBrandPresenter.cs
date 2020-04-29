using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Brands.EditBrand;
using TreniniDotNet.Common;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.Brands.EditBrand
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
