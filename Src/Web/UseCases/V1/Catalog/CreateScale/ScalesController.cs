using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ScalesController : UseCaseController<CreateScaleRequest, CreateScalePresenter>
    {
        public ScalesController(IMediator mediator, CreateScalePresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Post(CreateScaleRequest request)
        {
            return HandleRequest(request);
        }
    }
}