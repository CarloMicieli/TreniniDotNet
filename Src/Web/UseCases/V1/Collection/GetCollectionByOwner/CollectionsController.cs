using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetCollectionByOwner
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CollectionsController : UseCaseController<GetCollectionByOwnerRequest, GetCollectionByOwnerPresenter>
    {
        public CollectionsController(IMediator mediator, GetCollectionByOwnerPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet(Name = "GetCollectionByOwner")]
        [ProducesResponseType(typeof(GetCollectionByOwnerOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetCollectionByOwner(GetCollectionByOwnerRequest request)
        {
            return HandleRequest(request);
        }
    }
}
