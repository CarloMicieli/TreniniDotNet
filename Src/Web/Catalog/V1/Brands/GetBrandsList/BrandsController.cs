using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Web.Catalog.V1.Brands.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.UseCases;
using TreniniDotNet.Web.Infrastructure.ViewModels.Pagination;

namespace TreniniDotNet.Web.Catalog.V1.Brands.GetBrandsList
{
    [AllowAnonymous]
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