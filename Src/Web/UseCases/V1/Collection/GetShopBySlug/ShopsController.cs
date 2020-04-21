using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.GetShopBySlug;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetShopBySlug
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShopsController : UseCaseController<GetShopBySlugRequest, GetShopBySlugPresenter>
    {
        public ShopsController(IMediator mediator, GetShopBySlugPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet("{slug}")]
        [ProducesResponseType(typeof(GetShopBySlugOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Post(GetShopBySlugRequest request)
        {
            return HandleRequest(request);
        }
    }
}