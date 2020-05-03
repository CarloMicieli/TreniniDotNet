using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Common;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.EditRollingStock
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CatalogItemsController : UseCaseController<EditRollingStockRequest, EditRollingStockPresenter>
    {
        public CatalogItemsController(IMediator mediator, EditRollingStockPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPut("{slug}/rollingStocks/{rollingStockId}")]
        public Task<IActionResult> EditRollingStock(string slug, Guid rollingStockId, EditRollingStockRequest request)
        {
            request.Slug = Slug.Of(slug);
            request.RollingStockId = rollingStockId;

            return HandleRequest(request);
        }
    }
}
