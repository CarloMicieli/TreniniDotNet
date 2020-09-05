using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Brands.GetBrandsList;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Web.Catalog.V1.Brands.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;
using TreniniDotNet.Web.Infrastructure.ViewModels.Pagination;

namespace TreniniDotNet.Web.Catalog.V1.Brands.GetBrandsList
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

        private BrandView ToBrandView(Brand brand)
        {
            return new BrandView(brand,
                _linksGenerator.GenerateSelfLink(nameof(GetBrandBySlug.BrandsController.GetBrandBySlug), brand.Slug));
        }
    }
}
