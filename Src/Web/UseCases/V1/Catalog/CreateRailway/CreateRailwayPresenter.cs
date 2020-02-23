using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateRailway
{
    public sealed class CreateRailwayPresenter : DefaultHttpResultPresenter<CreateRailwayOutput>, ICreateRailwayOutputPort
    {
        public void RailwayAlreadyExists(string message)
        {
            ViewModel = BadRequest(message);
        }

        public override void Standard(CreateRailwayOutput output)
        {
            ViewModel = Created(
                "GetRailwayBySlug",
                new
                {
                    slug = output.Slug,
                    version = "1",
                },
                output);
        }
    }
}
