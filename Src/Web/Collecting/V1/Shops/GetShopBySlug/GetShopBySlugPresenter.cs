using System;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Shops.GetShopBySlug;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetShopBySlug
{
    public sealed class GetShopBySlugPresenter : DefaultHttpResultPresenter<GetShopBySlugOutput>, IGetShopBySlugOutputPort
    {
        private readonly ILinksGenerator _linksGenerator;

        public GetShopBySlugPresenter(ILinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator ?? throw new ArgumentNullException(nameof(linksGenerator));
        }

        public void ShopNotFound(Slug slug)
        {
            ViewModel = new NotFoundObjectResult(new { Slug = slug.Value });
        }

        public override void Standard(GetShopBySlugOutput output)
        {
            var selfLink = _linksGenerator.GenerateSelfLink(
                nameof(ShopsController.GetShopBySlug),
                output.Shop.Slug);

            ViewModel = new OkObjectResult(new ShopView(output.Shop, selfLink));
        }
    }
}