using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwayBySlug
{
    public class GetRailwayBySlugPresenter : DefaultHttpResultPresenter<GetRailwayBySlugOutput>, IGetRailwayBySlugOutputPort
    {
        public void RailwayNotFound(string message)
        {
            ViewModel = new NotFoundResult();
        }

        public override void Standard(GetRailwayBySlugOutput output)
        {
            var railwayViewModel = new RailwayView(output.Railway);
            ViewModel = new OkObjectResult(railwayViewModel);
        }
    }
}
