using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TreniniDotNet.Web.UseCases.V1.Collection.RemoveItemFromWishlist
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class WishlistsController : UseCaseController<RemoveItemFromWishlistRequest, RemoveItemFromWishlistPresenter>
    {
        public WishlistsController(IMediator mediator, RemoveItemFromWishlistPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpDelete("{id}/items/{itemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Get(Guid id, Guid itemId)
        {
            var request = new RemoveItemFromWishlistRequest
            {
                Id = id,
                ItemId = itemId,
                Owner = CurrentUser
            };
            return HandleRequest(request);
        }
    }
}
