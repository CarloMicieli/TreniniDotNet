using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.RemoveShopFromFavourites
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class ShopsController : UseCaseController<RemoveShopFromFavouritesRequest, RemoveShopFromFavouritesPresenter>
    {
        public ShopsController(IMediator mediator, RemoveShopFromFavouritesPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpDelete("favourites")]
        [ProducesResponseType(typeof(RemoveShopFromFavouritesOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Get(RemoveShopFromFavouritesRequest request)
        {
            return HandleRequest(request);
        }
    }
}
