using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.AddShopToFavourites
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class ShopsController : UseCaseController<AddShopToFavouritesRequest, AddShopToFavouritesPresenter>
    {
        public ShopsController(IMediator mediator, AddShopToFavouritesPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPost("favourites")]
        [ProducesResponseType(typeof(AddShopToFavouritesOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Post(AddShopToFavouritesRequest request)
        {
            return HandleRequest(request);
        }
    }
}
