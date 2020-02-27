using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Web.ViewModels.Pagination;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

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
        [ProducesResponseType(typeof(PaginatedViewModel<ScaleView>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetScales(int start = 0, int limit = 50)
        {
            return HandleRequest(new GetScalesListRequest(new Page(start, limit)));
        }
    }
}