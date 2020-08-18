using TreniniDotNet.Application.Collecting.Shops.CreateShop;

namespace TreniniDotNet.Application.Collecting.Shops
{
    public static class NewCreateShopInput
    {
        public static readonly CreateShopInput Empty = With();

        public static CreateShopInput With(
            string name = null, string websiteUrl = null,
            string emailAddress = null, ShopAddressInput shopAddress = null,
            string phoneNumber = null) =>
            new CreateShopInput(name, websiteUrl, emailAddress, shopAddress, phoneNumber);
    }
}