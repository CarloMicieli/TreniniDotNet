using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Web.ViewModels.Pagination;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandsList
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BrandsController : UseCaseController<GetBrandsListRequest, GetBrandsListPresenter>
    {
        public BrandsController(IMediator mediator, GetBrandsListPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedViewModel<BrandView>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetBrands(int start = 0, int limit = 50)
        {
            return HandleRequest(new GetBrandsListRequest(new Page(start, limit)));
        }
    }
}