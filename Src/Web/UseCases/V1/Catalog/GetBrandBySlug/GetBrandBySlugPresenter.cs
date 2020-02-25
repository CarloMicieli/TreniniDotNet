using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.Pagination;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug
{
    public class GetBrandBySlugPresenter : DefaultHttpResultPresenter<GetBrandBySlugOutput>, IGetBrandBySlugOutputPort
    {
        private readonly IPaginationLinksGenerator _linksGenerator;

        public GetBrandBySlugPresenter(IPaginationLinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator;
        }

        public void BrandNotFound(string message)
        {
            ViewModel = new NotFoundResult();
        }

        public override void Standard(GetBrandBySlugOutput output)
        {
            var selfLink = _linksGenerator.GenerateLink("GetBrandBySlug", new { slug = output.Brand.Slug });
            var brandViewModel = new BrandView(output.Brand, selfLink);
            ViewModel = new OkObjectResult(brandViewModel);
        }
    }
}
