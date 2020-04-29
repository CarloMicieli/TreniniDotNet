using System;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugPresenter : DefaultHttpResultPresenter<GetCatalogItemBySlugOutput>, IGetCatalogItemBySlugOutputPort
    {
        private readonly ILinksGenerator _linksGenerator;

        public GetCatalogItemBySlugPresenter(ILinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator ??
                throw new ArgumentNullException(nameof(linksGenerator));
        }

        public void CatalogItemNotFound(string message)
        {
            ViewModel = new NotFoundResult();
        }

        public override void Standard(GetCatalogItemBySlugOutput output)
        {
            var selfLink = _linksGenerator.GenerateSelfLink(
                nameof(CatalogItemsController.GetCatalogItemBySlug),
                output.Item.Slug);
            var brandViewModel = new CatalogItemView(output.Item, selfLink);
            ViewModel = new OkObjectResult(brandViewModel);
        }
    }
}