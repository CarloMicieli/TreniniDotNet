using Microsoft.AspNetCore.Mvc;
using System;
using TreniniDotNet.Application.Boundaries.Catalog.GetCatalogItemBySlug;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.Links;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetCatalogItemBySlug
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