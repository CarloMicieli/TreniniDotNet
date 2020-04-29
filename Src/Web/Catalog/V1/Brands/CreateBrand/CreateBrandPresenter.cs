using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.Brands.CreateBrand
{
    public class CreateBrandPresenter : DefaultHttpResultPresenter<CreateBrandOutput>, ICreateBrandOutputPort
    {
        public void BrandAlreadyExists(string message)
        {
            ViewModel = new ConflictObjectResult(message);
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
