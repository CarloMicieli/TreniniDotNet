using System;
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
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
            _presenter = presenter ??
                throw new ArgumentNullException(nameof(presenter));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateScaleRequest request)
        {
            await _mediator.Send(request);
            return _presenter.ViewModel;
        }
    }
}