using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.Brands.CreateBrand
{
    public sealed class CreateBrandPresenter : DefaultHttpResultPresenter<CreateBrandOutput>, ICreateBrandOutputPort
    {
        public void BrandAlreadyExists(Slug brand)
        {
            ViewModel = new ConflictObjectResult($"Brand '{brand.Value}' already exists");
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
