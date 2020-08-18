using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Railways.CreateRailway
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RailwaysController : UseCaseController<CreateRailwayRequest, CreateRailwayPresenter>
    {
        public RailwaysController(IMediator mediator, CreateRailwayPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateRailwayOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> PostRailway(CreateRailwayRequest brandRequest)
        {
            return HandleRequest(brandRequest);
        }
    }
}