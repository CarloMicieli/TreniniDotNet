using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandsList;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.Links;
using TreniniDotNet.Web.ViewModels.Pagination;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandsList
{
    public sealed class GetBrandsListPresenter : DefaultHttpResultPresenter<GetBrandsListOutput>, IGetBrandsListOutputPort
    {
        private readonly ILinksGenerator _linksGenerator;

        public GetBrandsListPresenter(ILinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator;
        }

        public override void Standard(GetBrandsListOutput output)
        {
            var links = _linksGenerator.Generate("GetBrands", output.PaginatedResult);

            PaginatedViewModel<BrandView> viewModel = output.PaginatedResult
                .ToViewModel(links, brand => {
                    return new BrandView(brand, _linksGenerator.GenerateSelfLink("GetBrandBySlug", brand.Slug));
                });

            ViewModel = new OkObjectResult(viewModel);
        }
    }
}
