using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistsByOwner
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class WishlistsController : UseCaseController<GetWishlistsByOwnerRequest, GetWishlistsByOwnerPresenter>
    {
        public WishlistsController(IMediator mediator, GetWishlistsByOwnerPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetWishlistsByOwnerOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Get(GetWishlistsByOwnerRequest request)
        {
            return HandleRequest(request);
        }
    }
}
