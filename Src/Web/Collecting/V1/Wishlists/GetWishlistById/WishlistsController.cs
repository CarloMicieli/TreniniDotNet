using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistById
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class WishlistsController : UseCaseController<GetWishlistByIdRequest, GetWishlistByIdPresenter>
    {
        public WishlistsController(IMediator mediator, GetWishlistByIdPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet("{id}", Name = nameof(GetWishlistById))]
        [ProducesResponseType(typeof(GetWishlistByIdOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetWishlistById(Guid id)
        {
            var request = new GetWishlistByIdRequest
            {
                Id = id,
                Owner = CurrentUser
            };

            return HandleRequest(request);
        }
    }
}
