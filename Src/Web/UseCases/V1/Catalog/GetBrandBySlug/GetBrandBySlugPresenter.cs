using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.GetBrandBySlug;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug
{
    public class GetBrandBySlugPresenter : IOutputPort
    {
        public ActionResult<BrandView> ViewModel { get; private set; } = null!;

        public void BrandNotFound(string message)
        {
            ViewModel = new NotFoundResult();
        }

        public void Standard(GetBrandBySlugOutput output)
        {
            if (output.Brand is null)
            {
                ViewModel = new NotFoundResult();
            }
            else
            {
                var brandViewModel = new BrandView(output.Brand);
                ViewModel = new ActionResult<BrandView>(brandViewModel);
            }
        }
    }
}
