using System;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Shops.GetShopsList;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;
using TreniniDotNet.Web.Infrastructure.ViewModels.Pagination;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetShopsList
{
    public sealed class GetShopsListPresenter : DefaultHttpResultPresenter<GetShopsListOutput>, IGetShopsListOutputPort
    {
        private readonly ILinksGenerator _linksGenerator;

        public GetShopsListPresenter(ILinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator ?? throw new ArgumentNullException(nameof(linksGenerator));
        }

        public override void Standard(GetShopsListOutput output)

        {
            var links = _linksGenerator.Generate(nameof(ShopsController.GetShopsList), output.PaginatedResult);

            PaginatedViewModel<ShopView> viewModel = output
                .PaginatedResult
                .ToViewModel(links, ToShopView);

            ViewModel = new OkObjectResult(viewModel);
        }

        private ShopView ToShopView(Shop shop)
        {
            return new ShopView(shop,
                _linksGenerator.GenerateSelfLink(nameof(GetShopBySlug.ShopsController.GetShopBySlug),
                    shop.Slug));
        }
    }
}