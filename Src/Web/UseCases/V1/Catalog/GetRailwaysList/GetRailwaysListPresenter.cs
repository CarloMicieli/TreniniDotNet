using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwaysList;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.Links;
using TreniniDotNet.Web.ViewModels.Pagination;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwaysList
{
    public sealed class GetRailwaysListPresenter : DefaultHttpResultPresenter<GetRailwaysListOutput>, IGetRailwaysListOutputPort
    {
        private readonly ILinksGenerator _linksGenerator;

        public GetRailwaysListPresenter(ILinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator;
        }

        public override void Standard(GetRailwaysListOutput output)
        {
            var links = _linksGenerator.Generate("GetRailways", output.PaginatedResult);

            PaginatedViewModel<RailwayView> viewModel = output.PaginatedResult
                .ToViewModel(links, railway => {
                    return new RailwayView(railway, _linksGenerator.GenerateSelfLink("GetRailwayBySlug", railway.Slug));
                });

            ViewModel = new OkObjectResult(viewModel);
        }
    }
}