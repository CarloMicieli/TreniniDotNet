using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CreateBrandPresenter _presenter;

        public BrandsController(IMediator mediator, CreateBrandPresenter presenter)
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
        public async Task<IActionResult> Post(CreateBrandRequest request)
        {
            await _mediator.Send(request);
            return _presenter.ViewModel;
        }
    }
}