using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.GetShopsList;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetShopsList
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShopsController : UseCaseController<GetShopsListRequest, GetShopsListPresenter>
    {
        public ShopsController(IMediator mediator, GetShopsListPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetShopsListOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Post(GetShopsListRequest request)
        {
            return HandleRequest(request);
        }
    }
}