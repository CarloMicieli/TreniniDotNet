namespace TreniniDotNet.Application.Collecting.Shops
{
    public static class NewShopAddressInput
    {
        public static readonly ShopAddressInput Empty = With();

        public static ShopAddressInput With(
            string line1 = null,
            string line2 = null,
            string city = null,
            string region = null,
            string postalCode = null,
            string country = null) => new ShopAddressInput
            {
                Line1 = line1,
                Line2 = line2,
                City = city,
                Region = region,
                PostalCode = postalCode,
                Country = country
            };
    }
}