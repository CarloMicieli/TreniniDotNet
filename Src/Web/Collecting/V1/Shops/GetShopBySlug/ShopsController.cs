using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetShopBySlug
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ShopsController : UseCaseController<GetShopBySlugRequest, GetShopBySlugPresenter>
    {
        public ShopsController(IMediator mediator, GetShopBySlugPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        [Route("{slug}", Name = nameof(GetShopBySlug))]
        [ProducesResponseType(typeof(ShopView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetShopBySlug(string slug)
        {
            var request = new GetShopBySlugRequest(slug);
            return HandleRequest(request);
        }
    }
}
