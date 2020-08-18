using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockPresenter : DefaultHttpResultPresenter<EditRollingStockOutput>, IEditRollingStockOutputPort
    {
        public override void Standard(EditRollingStockOutput output)
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

        public void RailwayWasNotFound(Slug slug)
        {
            ViewModel = new UnprocessableEntityObjectResult(new { Slug = slug.Value });
        }

        public void RollingStockWasNotFound(Slug slug, RollingStockId rollingStockId)
        {
            ViewModel = new NotFoundObjectResult(new
            {
                Slug = slug.Value,
                RollingStockId = rollingStockId
            });
        }

        public void CatalogItemWasNotFound(Slug slug)
        {
            ViewModel = new NotFoundObjectResult(new { Slug = slug.Value });
        }
    }
}
