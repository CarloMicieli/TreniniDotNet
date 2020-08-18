using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.GetLatestCatalogItems
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CatalogItemsController : UseCaseController<GetLatestCatalogItemsRequest, GetLatestCatalogItemsPresenter>
    {
        public CatalogItemsController(IMediator mediator, GetLatestCatalogItemsPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet("latest", Name = nameof(GetLatestCatalogItems))]
        [ProducesResponseType(typeof(CatalogItemView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetLatestCatalogItems(int start = 0, int limit = 50)
        {
            return HandleRequest(new GetLatestCatalogItemsRequest
            {
                Page = new Page(start, limit)
            });
        }
    }
}
