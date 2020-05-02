using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.CatalogItems.GetLatestCatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;
using TreniniDotNet.Web.Infrastructure.ViewModels.Pagination;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.GetLatestCatalogItems
{
    public sealed class GetLatestCatalogItemsPresenter : DefaultHttpResultPresenter<GetLatestCatalogItemsOutput>, IGetLatestCatalogItemsOutputPort
    {
        private readonly ILinksGenerator _linksGenerator;

        public GetLatestCatalogItemsPresenter(ILinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator;
        }

        public override void Standard(GetLatestCatalogItemsOutput output)
        {
            var links = _linksGenerator.Generate(nameof(CatalogItemsController.GetLatestCatalogItems), output.PaginatedResult);

            PaginatedViewModel<CatalogItemView> viewModel = output
                .PaginatedResult
                .ToViewModel(links, ToCatalogItemView);

            ViewModel = new OkObjectResult(viewModel);
        }

        private CatalogItemView ToCatalogItemView(ICatalogItem catalogItem)
        {
            return new CatalogItemView(catalogItem, _linksGenerator.GenerateSelfLink(
                nameof(GetCatalogItemBySlug.CatalogItemsController.GetCatalogItemBySlug),
                catalogItem.Slug));
        }
    }
}
