using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.EditRailway;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditRailway
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class RailwaysController : UseCaseController<EditRailwayRequest, EditRailwayPresenter>
    {
        public RailwaysController(IMediator mediator, EditRailwayPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPut("{slug}")]
        [ProducesResponseType(typeof(EditRailwayOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> EditRailway(string slug, EditRailwayRequest request)
        {
            request.Slug = Slug.Of(slug);
            return HandleRequest(request);
        }
    }
}
