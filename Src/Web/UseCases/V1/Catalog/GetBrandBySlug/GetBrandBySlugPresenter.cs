using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug
{
    public class GetBrandBySlugPresenter : DefaultHttpResultPresenter<GetBrandBySlugOutput>, IGetBrandBySlugOutputPort
    {
        public void BrandNotFound(string message)
        {
            ViewModel = new NotFoundResult();
        }

        public override void Standard(GetBrandBySlugOutput output)
        {
            var brandViewModel = new BrandView(output.Brand);
            ViewModel = new OkObjectResult(brandViewModel);
        }
    }
}
