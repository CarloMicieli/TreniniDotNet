using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug;
using TreniniDotNet.Web.Catalog.V1.Railways.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Catalog.V1.Railways.GetRailwayBySlug
{
    public class GetRailwayBySlugPresenter : DefaultHttpResultPresenter<GetRailwayBySlugOutput>, IGetRailwayBySlugOutputPort
    {
        private readonly ILinksGenerator _linksGenerator;

        public GetRailwayBySlugPresenter(ILinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator;
        }

        public void RailwayNotFound(string message)
        {
            ViewModel = new NotFoundResult();
        }

        public override void Standard(GetRailwayBySlugOutput output)
        {
            var selfLink = _linksGenerator.GenerateSelfLink(
                nameof(RailwaysController.GetRailwayBySlug),
                output.Railway.Slug);

            var railwayViewModel = new RailwayView(output.Railway, selfLink);
            ViewModel = new OkObjectResult(railwayViewModel);
        }
    }
}
