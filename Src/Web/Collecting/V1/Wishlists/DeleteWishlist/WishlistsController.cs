using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.DeleteWishlist
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class WishlistsController : UseCaseController<DeleteWishlistRequest, DeleteWishlistPresenter>
    {
        public WishlistsController(IMediator mediator, DeleteWishlistPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteWishlistRequest
            {
                Id = id,
                Owner = CurrentUser
            };
            return HandleRequest(request);
        }
    }
}
