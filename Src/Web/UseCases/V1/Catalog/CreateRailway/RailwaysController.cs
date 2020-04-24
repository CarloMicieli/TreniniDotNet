using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateRailway
{
    [AllowAnonymous]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> PostRailway(CreateRailwayRequest brandRequest)
        {
            return HandleRequest(brandRequest);
        }
    }
}