using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    public class CreateScalePresenter : DefaultHttpResultPresenter<CreateScaleOutput>, ICreateScaleOutputPort
    {
        public void ScaleAlreadyExists(string message)
        {
            ViewModel = BadRequest(message);
        }

        public override void Standard(CreateScaleOutput output)
        {
            ViewModel = new CreatedAtRouteResult(
                "GetScale",
                new
                {
                    slug = output.Slug,
                    version = "1.0",
                },
                output);
        }
    }
}