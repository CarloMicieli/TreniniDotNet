using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.RemoveItemFromCollection
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CollectionsController : UseCaseController<RemoveItemFromCollectionRequest, RemoveItemFromCollectionPresenter>
    {
        public CollectionsController(IMediator mediator, RemoveItemFromCollectionPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpDelete("items")]
        [ProducesResponseType(typeof(RemoveItemFromCollectionOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Put(RemoveItemFromCollectionRequest request)
        {
            return HandleRequest(request);
        }
    }
}
