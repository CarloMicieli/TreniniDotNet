using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug;
using TreniniDotNet.Web.Catalog.V1.Brands.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Catalog.V1.Brands.GetBrandBySlug
{
    public class GetBrandBySlugPresenter : DefaultHttpResultPresenter<GetBrandBySlugOutput>, IGetBrandBySlugOutputPort
    {
        private readonly ILinksGenerator _linksGenerator;

        public GetBrandBySlugPresenter(ILinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator;
        }

        public void BrandNotFound(string message)
        {
            ViewModel = new NotFoundResult();
        }

        public override void Standard(GetBrandBySlugOutput output)
        {
            var selfLink = _linksGenerator.GenerateSelfLink(
                nameof(BrandsController.GetBrandBySlug),
                output.Brand.Slug);
            var brandViewModel = new BrandView(output.Brand, selfLink);
            ViewModel = new OkObjectResult(brandViewModel);
        }
    }
}
