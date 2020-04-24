using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.CreateCollection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateCollection
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CollectionsController : UseCaseController<CreateCollectionRequest, CreateCollectionPresenter>
    {
        public CollectionsController(IMediator mediator, CreateCollectionPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateCollectionOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Post(CreateCollectionRequest request)
        {
            request.Owner = HttpContext.User.Identity.Name;
            return HandleRequest(request);
        }
    }
}
