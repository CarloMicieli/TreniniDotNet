using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Railways.GetRailwaysList;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Web.Catalog.V1.Railways.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;
using TreniniDotNet.Web.Infrastructure.ViewModels.Pagination;

namespace TreniniDotNet.Web.Catalog.V1.Railways.GetRailwaysList
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
            var links = _linksGenerator.Generate(nameof(RailwaysController.GetRailways), output.PaginatedResult);

            PaginatedViewModel<RailwayView> viewModel = output
                .PaginatedResult
                .ToViewModel(links, ToRailwayView);

            ViewModel = new OkObjectResult(viewModel);
        }

        private RailwayView ToRailwayView(IRailway railway)
        {
            return new RailwayView(railway, _linksGenerator.GenerateSelfLink(
                nameof(GetRailwayBySlug.RailwaysController.GetRailwayBySlug),
                railway.Slug));
        }
    }
}