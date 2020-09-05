using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels;
using TreniniDotNet.Web.Collecting.V1.Shops.GetShopBySlug;
using TreniniDotNet.Web.Infrastructure.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetFavouriteShops
{
    public sealed class GetFavouriteShopsPresenter : DefaultHttpResultPresenter<GetFavouriteShopsOutput>, IGetFavouriteShopsOutputPort
    {
        private readonly ILinksGenerator _linksGenerator;

        public GetFavouriteShopsPresenter(ILinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator ?? throw new ArgumentNullException(nameof(linksGenerator));
        }

        public override void Standard(GetFavouriteShopsOutput output)
        {
            var shops = output.FavouriteShops
                .Select(ToShopView);

            ViewModel = new OkObjectResult(new FavouriteShopsView(output.Owner, shops));
        }

        private ShopView ToShopView(Shop shop)
        {
            var selfLink = _linksGenerator.GenerateSelfLink(
                nameof(ShopsController.GetShopBySlug),
                shop.Slug);

            return new ShopView(shop, selfLink);
        }
    }
}
