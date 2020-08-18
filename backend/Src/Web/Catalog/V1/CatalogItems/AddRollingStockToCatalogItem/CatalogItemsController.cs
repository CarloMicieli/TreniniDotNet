using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.Requests;
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
        [ProducesResponseType(typeof(AddRollingStockToCatalogItemOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> PostRollingStock(string slug, RollingStockRequest request)
        {
            return HandleRequest(new AddRollingStockToCatalogItemRequest
            {
                Slug = Slug.Of(slug),
                RollingStock = request
            });
        }
    }
}
