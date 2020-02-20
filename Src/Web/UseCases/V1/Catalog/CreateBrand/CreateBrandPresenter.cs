using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
{
    public class CreateBrandPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NotFoundResult();

        public void BrandAlreadyExists(string message)
        {
            ViewModel = new BadRequestObjectResult(message);
        }

        public void Standard(CreateBrandOutput output)
        {
            ViewModel = new CreatedAtRouteResult(
                "GetBrand",
                new
                {
                    slug = output.Slug,
                    version = "1.0",
                },
                output);
        }
    }
}
