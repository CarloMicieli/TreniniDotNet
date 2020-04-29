using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.Railways.CreateRailway
{
    public sealed class CreateRailwayPresenter : DefaultHttpResultPresenter<CreateRailwayOutput>, ICreateRailwayOutputPort
    {
        public void RailwayAlreadyExists(string message)
        {
            ViewModel = new ConflictObjectResult(message);
        }

        public override void Standard(CreateRailwayOutput output)
        {
            ViewModel = Created(
                nameof(GetRailwayBySlug.RailwaysController.GetRailwayBySlug),
                new
                {
                    slug = output.Slug,
                    version = "1",
                },
                output);
        }
    }
}
