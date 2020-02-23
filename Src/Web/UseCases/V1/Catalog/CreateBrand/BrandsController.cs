using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BrandsController : UseCaseController<CreateBrandRequest, CreateBrandPresenter>
    {
        public BrandsController(IMediator mediator, CreateBrandPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Post(CreateBrandRequest request)
        {
            return HandleRequest(request);
        }
    }
}