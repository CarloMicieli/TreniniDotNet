using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Web.ViewModels.Pagination;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwaysList
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RailwaysController : UseCaseController<GetRailwaysListRequest, GetRailwaysListPresenter>
    {
        public RailwaysController(IMediator mediator, GetRailwaysListPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedViewModel<RailwayView>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetRailways(int start = 0, int limit = 50)
        {
            return HandleRequest(new GetRailwaysListRequest(new Page(start, limit)));
        }
    }
}