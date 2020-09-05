using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Web.Catalog.V1.Railways.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.UseCases;
using TreniniDotNet.Web.Infrastructure.ViewModels.Pagination;

namespace TreniniDotNet.Web.Catalog.V1.Railways.GetRailwaysList
{
    [AllowAnonymous]
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