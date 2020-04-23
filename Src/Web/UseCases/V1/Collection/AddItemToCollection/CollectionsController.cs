using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddItemToCollection
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CollectionsController : UseCaseController<AddItemToCollectionRequest, AddItemToCollectionPresenter>
    {
        public CollectionsController(IMediator mediator, AddItemToCollectionPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPost("items")]
        [ProducesResponseType(typeof(AddItemToCollectionOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Post(AddItemToCollectionRequest request)
        {
            request.CollectionOwner = HttpContext.User.Identity.Name;
            return HandleRequest(request);
        }
    }
}
