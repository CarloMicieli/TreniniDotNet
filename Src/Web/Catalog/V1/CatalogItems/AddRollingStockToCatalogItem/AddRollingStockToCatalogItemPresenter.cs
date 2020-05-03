using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem;
using TreniniDotNet.Common;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.AddRollingStockToCatalogItem
{
    public sealed class AddRollingStockToCatalogItemPresenter : DefaultHttpResultPresenter<AddRollingStockToCatalogItemOutput>, IAddRollingStockToCatalogItemOutputPort
    {
        public override void Standard(AddRollingStockToCatalogItemOutput output)
        {
            ViewModel = Created(
                nameof(GetCatalogItemBySlug.CatalogItemsController.GetCatalogItemBySlug),
                new
                {
                    slug = output.Slug,
                    version = "1",
                },
                output);
        }

        public void CatalogItemWasNotFound(Slug itemSlug)
        {
            ViewModel = new NotFoundObjectResult(new { Slug = itemSlug.Value });
        }

        public void RailwayWasNotFound(Slug railwaySlug)
        {
            ViewModel = new UnprocessableEntityObjectResult(new { Slug = railwaySlug.Value });
        }
    }
}
