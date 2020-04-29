using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.Scales.CreateScale
{
    public sealed class CreateScalePresenter : DefaultHttpResultPresenter<CreateScaleOutput>, ICreateScaleOutputPort
    {
        public void ScaleAlreadyExists(string message)
        {
            ViewModel = new ConflictObjectResult(message);
        }

        public override void Standard(CreateScaleOutput output)
        {
            ViewModel = new CreatedAtRouteResult(
                nameof(GetScaleBySlug.ScalesController.GetScaleBySlug),
                new
                {
                    slug = output.Slug,
                    version = "1",
                },
                output);
        }
    }
}