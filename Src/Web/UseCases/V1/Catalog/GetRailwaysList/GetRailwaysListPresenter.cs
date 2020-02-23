using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwaysList;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwaysList
{
    public sealed class GetRailwaysListPresenter : DefaultHttpResultPresenter<GetRailwaysListOutput>, IGetRailwaysListOutputPort
    {
        public override void Standard(GetRailwaysListOutput output)
        {
            var railwaysViewModel = output.Railways
                .Select(r => new RailwayView(r))
                .ToList();

            ViewModel = new OkObjectResult(railwaysViewModel);
        }
    }
}