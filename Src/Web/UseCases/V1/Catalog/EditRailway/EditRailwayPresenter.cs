using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.EditRailway;
using TreniniDotNet.Common;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditRailway
{
    public sealed class EditRailwayPresenter : DefaultHttpResultPresenter<EditRailwayOutput>, IEditRailwayOutputPort
    {
        public void RailwayNotFound(Slug slug)
        {
            ViewModel = new NotFoundObjectResult(new { Slug = slug });
        }

        public override void Standard(EditRailwayOutput output)
        {
            ViewModel = new OkResult();
        }
    }
}
