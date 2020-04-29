using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Collections.AddItemToCollection;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Collections.AddItemToCollection
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

        [HttpPost("{id}/items")]
        [ProducesResponseType(typeof(AddItemToCollectionOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Post(Guid id, AddItemToCollectionRequest request)
        {
            request.Id = id;
            request.Owner = CurrentUser;

            return HandleRequest(request);
        }
    }
}
