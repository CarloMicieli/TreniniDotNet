using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpDelete("{id}/items/{itemId}")]
        [ProducesResponseType(typeof(RemoveItemFromCollectionOutput), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Put(Guid id, Guid itemId)
        {
            var request = new RemoveItemFromCollectionRequest
            {
                Id = id,
                ItemId = itemId,
                Owner = CurrentUser
            };

            return HandleRequest(request);
        }
    }
}
