using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public Task<IActionResult> GetRailwaysList()
        {
            return HandleRequest(new GetRailwaysListRequest());
        }
    }
}