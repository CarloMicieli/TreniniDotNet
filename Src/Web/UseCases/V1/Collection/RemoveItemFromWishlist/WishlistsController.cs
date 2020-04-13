﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;

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

        [HttpDelete("{id}/items")]
        [ProducesResponseType(typeof(RemoveItemFromWishlistOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Get(RemoveItemFromWishlistRequest request)
        {
            return HandleRequest(request);
        }
    }
}