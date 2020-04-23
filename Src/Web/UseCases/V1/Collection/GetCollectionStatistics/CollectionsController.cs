using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetCollectionStatistics
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CollectionsController : UseCaseController<GetCollectionStatisticsRequest, GetCollectionStatisticsPresenter>
    {
        public CollectionsController(IMediator mediator, GetCollectionStatisticsPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet("statistics")]
        [ProducesResponseType(typeof(GetCollectionStatisticsOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Get()
        {
            var request = new GetCollectionStatisticsRequest();
            request.Owner = HttpContext.User.Identity.Name;

            return HandleRequest(request);
        }
    }
}
