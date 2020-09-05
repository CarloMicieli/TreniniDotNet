using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner;
using TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistsByOwner
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

        [HttpGet("owner/{owner}")]
        [ProducesResponseType(typeof(WishlistsView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetWishlists(string owner, [FromQuery] string visibility = "all")
        {
            var request = new GetWishlistsByOwnerRequest
            {
                Owner = owner,
                Visibility = visibility,
                UserIsOwner = (CurrentUser == owner)
            };
            return HandleRequest(request);
        }
    }
}
