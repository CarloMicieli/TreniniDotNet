using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem;

namespace TreniniDotNet.Web.UseCases.V1.Collection.EditCollectionItem
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CollectionsController : UseCaseController<EditCollectionItemRequest, EditCollectionItemPresenter>
    {
        public CollectionsController(IMediator mediator, EditCollectionItemPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPut("items")]
        [ProducesResponseType(typeof(EditCollectionItemOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Put(EditCollectionItemRequest request)
        {
            return HandleRequest(request);
        }
    }
}
