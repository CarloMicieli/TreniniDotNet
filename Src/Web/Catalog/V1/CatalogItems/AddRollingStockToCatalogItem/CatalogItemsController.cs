using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.AddRollingStockToCatalogItem
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CatalogItemsController : UseCaseController<AddRollingStockToCatalogItemRequest, AddRollingStockToCatalogItemPresenter>
    {
        public CatalogItemsController(IMediator mediator, AddRollingStockToCatalogItemPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPost("{slug}/rollingStocks")]
        public Task<IActionResult> PostRollingStock(string slug, AddRollingStockToCatalogItemRequest request)
        {
            return HandleRequest(request);
        }
    }
}
