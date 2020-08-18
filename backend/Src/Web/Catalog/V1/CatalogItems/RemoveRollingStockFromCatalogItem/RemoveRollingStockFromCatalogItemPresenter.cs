using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public sealed class RemoveRollingStockFromCatalogItemPresenter : DefaultHttpResultPresenter<RemoveRollingStockFromCatalogItemOutput>, IRemoveRollingStockFromCatalogItemOutputPort
    {
        public override void Standard(RemoveRollingStockFromCatalogItemOutput output)
        {
            ViewModel = new NoContentResult();
        }

        public void RollingStockWasNotFound(Slug slug, RollingStockId rollingStockId)
        {
            ViewModel = new NotFoundObjectResult(new
            {
                Slug = slug.Value,
                RollingStockId = rollingStockId
            });
        }
    }
}
