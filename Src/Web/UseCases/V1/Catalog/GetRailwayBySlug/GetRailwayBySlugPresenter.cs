using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.Links;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwayBySlug
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
            var selfLink = _linksGenerator.GenerateSelfLink("GetRailwayBySlug", output.Railway.Slug);
            var railwayViewModel = new RailwayView(output.Railway, selfLink);
            ViewModel = new OkObjectResult(railwayViewModel);
        }
    }
}
