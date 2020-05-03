using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.RemoveRollingStockFromCatalogItem
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CatalogItemsController : UseCaseController<RemoveRollingStockFromCatalogItemRequest, RemoveRollingStockFromCatalogItemPresenter>
    {
        public CatalogItemsController(IMediator mediator, RemoveRollingStockFromCatalogItemPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpDelete("{slug}/rollingStocks/{rollingStockId}")]
        public Task<IActionResult> DeleteRollingStock(string slug, Guid rollingStockId)
        {
            return HandleRequest(new RemoveRollingStockFromCatalogItemRequest { });
        }
    }
}
