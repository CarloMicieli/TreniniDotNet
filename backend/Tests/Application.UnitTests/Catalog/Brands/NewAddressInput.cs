namespace TreniniDotNet.Application.Catalog.Brands
{
    public static class NewAddressInput
    {
        public static readonly AddressInput Empty = With();

        public static AddressInput With(
            string line1 = null,
            string line2 = null,
            string city = null,
            string region = null,
            string postalCode = null,
            string country = null) => new AddressInput
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
