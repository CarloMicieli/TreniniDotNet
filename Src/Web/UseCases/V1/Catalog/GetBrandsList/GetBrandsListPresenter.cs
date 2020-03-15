using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandsList;
using TreniniDotNet.Domain.Catalog.Brands;
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
            var links = _linksGenerator.Generate(nameof(BrandsController.GetBrands), output.PaginatedResult);

            PaginatedViewModel<BrandView> viewModel = output
                .PaginatedResult
                .ToViewModel(links, ToBrandView);

            ViewModel = new OkObjectResult(viewModel);
        }

        private BrandView ToBrandView(IBrand brand)
        {
            return new BrandView(brand,
                _linksGenerator.GenerateSelfLink(nameof(GetBrandBySlug.BrandsController.GetBrandBySlug), brand.Slug));
        }
    }
}
