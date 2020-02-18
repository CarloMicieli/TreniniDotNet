using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.CreateScale;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    public class CreateScalePresenter
    {
        public IActionResult ViewModel { get; private set; } = new NotFoundResult();

        public void ScaleAlreadyExists(string message)
        {
            ViewModel = new BadRequestObjectResult(message);
        }

        public void Standard(CreateScaleOutput output)
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