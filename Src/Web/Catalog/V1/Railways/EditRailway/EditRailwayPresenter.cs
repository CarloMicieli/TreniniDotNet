using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Railways.EditRailway;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.Railways.EditRailway
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
