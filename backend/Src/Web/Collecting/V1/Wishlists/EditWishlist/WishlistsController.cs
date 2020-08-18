using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlist
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class WishlistsController : UseCaseController<EditWishlistRequest, EditWishlistPresenter>
    {
        public WishlistsController(IMediator mediator, EditWishlistPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CreateWishlistOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Put(Guid id, EditWishlistRequest request)
        {
            request.Id = id;
            request.Owner = CurrentUser;

            return HandleRequest(request);
        }
    }
}
