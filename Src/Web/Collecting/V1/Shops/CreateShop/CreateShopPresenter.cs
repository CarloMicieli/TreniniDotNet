using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Shops.CreateShop;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Shops.CreateShop
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
