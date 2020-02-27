using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
{
    public class CreateBrandPresenter : DefaultHttpResultPresenter<CreateBrandOutput>, ICreateBrandOutputPort
    {
        public void BrandAlreadyExists(string message)
        {
            ViewModel = new BadRequestObjectResult(message);
        }
        
        public override void Standard(CreateBrandOutput output)
        {
            ViewModel = Created(
                nameof(GetBrandBySlug.BrandsController.GetBrandBySlug),
                new
                {
                    slug = output.Slug,
                    version = "1",
                },
                output);
        }
    }
}
