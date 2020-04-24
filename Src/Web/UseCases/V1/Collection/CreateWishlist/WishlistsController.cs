using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.CreateWishlist;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateWishlist
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class WishlistsController : UseCaseController<CreateWishlistRequest, CreateWishlistPresenter>
    {
        public WishlistsController(IMediator mediator, CreateWishlistPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateWishlistOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Post(CreateWishlistRequest request)
        {
            request.Owner = CurrentUser;

            return HandleRequest(request);
        }
    }
}
