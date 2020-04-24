using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddItemToWishlist
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class WishlistsController : UseCaseController<AddItemToWishlistRequest, AddItemToWishlistPresenter>
    {
        public WishlistsController(IMediator mediator, AddItemToWishlistPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPost("{id}/items")]
        [ProducesResponseType(typeof(AddItemToWishlistOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Post(Guid id, AddItemToWishlistRequest request)
        {
            request.Id = id;
            request.Owner = CurrentUser;

            return HandleRequest(request);
        }
    }
}
