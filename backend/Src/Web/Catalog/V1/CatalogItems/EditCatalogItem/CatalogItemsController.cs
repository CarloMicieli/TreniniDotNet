using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.EditCatalogItem
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CatalogItemsController : UseCaseController<EditCatalogItemRequest, EditCatalogItemPresenter>
    {
        public CatalogItemsController(IMediator mediator, EditCatalogItemPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPut("{slug}")]
        [ProducesResponseType(typeof(EditCatalogItemOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> EditCatalogItem(string slug, EditCatalogItemRequest request)
        {
            request.Slug = Slug.Of(slug);
            return HandleRequest(request);
        }
    }
}
