using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlistItem
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class WishlistsController : UseCaseController<EditWishlistItemRequest, EditWishlistItemPresenter>
    {
        public WishlistsController(IMediator mediator, EditWishlistItemPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPut("{id}/items/{itemId}")]
        [ProducesResponseType(typeof(EditWishlistItemOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> EditWishlistItem(Guid id, Guid itemId)
        {
            var request = new EditWishlistItemRequest
            {
                Id = id,
                ItemId = itemId,
                Owner = CurrentUser
            };
            return HandleRequest(request);
        }
    }
}
