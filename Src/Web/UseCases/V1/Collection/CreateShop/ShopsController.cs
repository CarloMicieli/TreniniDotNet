using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.CreateShop;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateShop
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class ShopsController : UseCaseController<CreateShopRequest, CreateShopPresenter>
    {
        public ShopsController(IMediator mediator, CreateShopPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateShopOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Post(CreateShopRequest request)
        {
            return HandleRequest(request);
        }
    }
}
