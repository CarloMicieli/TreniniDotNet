using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Shops.GetShopsList;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetShopsList
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
        public Task<IActionResult> Post(int start = 0, int limit = 50)
        {
            var request = new GetShopsListRequest(new Page(start, limit));
            return HandleRequest(request);
        }
    }
}