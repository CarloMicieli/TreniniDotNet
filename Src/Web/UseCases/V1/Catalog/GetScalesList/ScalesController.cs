using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScalesList
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ScalesController : UseCaseController<GetScalesListRequest, GetScalesListPresenter>
    {
        public ScalesController(IMediator mediator, GetScalesListPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        public Task<IActionResult> GetScalesList()
        {
            return HandleRequest(new GetScalesListRequest());
        }
    }
}