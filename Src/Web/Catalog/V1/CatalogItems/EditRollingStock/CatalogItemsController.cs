using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
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
        [ProducesResponseType(typeof(EditRollingStockOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> EditRollingStock(string slug, Guid rollingStockId, EditRollingStockRequest request)
        {
            request.Slug = Slug.Of(slug);
            request.RollingStockId = new RollingStockId(rollingStockId);

            return HandleRequest(request);
        }
    }
}
