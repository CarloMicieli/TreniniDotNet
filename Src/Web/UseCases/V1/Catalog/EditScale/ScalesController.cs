using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.EditScale;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditScale
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class ScalesController : UseCaseController<EditScaleRequest, EditScalePresenter>
    {
        public ScalesController(IMediator mediator, EditScalePresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPut("{slug}")]
        [ProducesResponseType(typeof(EditScaleOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> EditScale(string slug, EditScaleRequest request)
        {
            return HandleRequest(request);
        }
    }
}
