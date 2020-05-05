using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.Common;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.Scales.CreateScale
{
    public sealed class CreateScalePresenter : DefaultHttpResultPresenter<CreateScaleOutput>, ICreateScaleOutputPort
    {
        public void ScaleAlreadyExists(Slug slug)
        {
            ViewModel = new ConflictObjectResult($"Scale {slug.Value} already exists");
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
