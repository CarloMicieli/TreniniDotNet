using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ScalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CreateScalePresenter _presenter;

        public ScalesController(IMediator mediator, CreateScalePresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        /// <summary>
        /// Create a new brand
        /// </summary>
        /// <response code="200">The new brand was created successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Error.</response>
        /// <param name="request">The request to create a new brand.</param>
        /// <returns>The newly created brand.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateScaleResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateScaleRequest request)
        {
            await _mediator.Send(request);
            return _presenter.ViewModel;
        }
    }
}