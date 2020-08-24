using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetFavouriteShops
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class FavouriteShopsController : UseCaseController<GetFavouriteShopsRequest, GetFavouriteShopsPresenter>
    {
        public FavouriteShopsController(IMediator mediator, GetFavouriteShopsPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(FavouriteShopsView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Get()
        {
            var request = new GetFavouriteShopsRequest
            {
                Owner = CurrentUser
            };
            return HandleRequest(request);
        }
    }
}
