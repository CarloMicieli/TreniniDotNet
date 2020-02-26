using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Pagination;

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
        public Task<IActionResult> GetScales(int start = 0, int limit = 50)
        {
            return HandleRequest(new GetScalesListRequest(new Page(start, limit)));
        }
    }
}