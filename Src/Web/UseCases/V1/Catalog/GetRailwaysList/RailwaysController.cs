using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Pagination;

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
        public Task<IActionResult> GetRailways(int start = 0, int limit = 50)
        {
            return HandleRequest(new GetRailwaysListRequest(new Page(start, limit)));
        }
    }
}