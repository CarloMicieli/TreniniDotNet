using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Common;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetCatalogItemBySlug
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CatalogItemsController : UseCaseController<GetCatalogItemBySlugRequest, GetCatalogItemBySlugPresenter>
    {
        public CatalogItemsController(IMediator mediator, GetCatalogItemBySlugPresenter presenter) 
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        [Route("{slug}", Name = nameof(GetCatalogItemBySlug))]
        [ProducesResponseType(typeof(CatalogItemView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetCatalogItemBySlug(string slug)
        {
            return HandleRequest(new GetCatalogItemBySlugRequest(Slug.Of(slug)));
        }
    }
}