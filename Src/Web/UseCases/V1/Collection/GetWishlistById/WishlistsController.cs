using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistById;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistById
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

        [HttpGet("{id}", Name = "GetWishlistById")]
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
