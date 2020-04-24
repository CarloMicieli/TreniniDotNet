using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.CreateShop;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateShop
{
    public sealed class CreateShopPresenter : DefaultHttpResultPresenter<CreateShopOutput>, ICreateShopOutputPort
    {
        public void ShopAlreadyExists(string shopName)
        {
            ViewModel = new ConflictObjectResult(new
            {
                Shop = shopName
            });
        }

        public override void Standard(CreateShopOutput output)
        {
            ViewModel = Created(
                nameof(GetShopBySlug.ShopsController.GetShopBySlug),
                new
                {
                    Slug = output.Slug.Value,
                    version = "1",
                },
                output);
        }
    }
}
