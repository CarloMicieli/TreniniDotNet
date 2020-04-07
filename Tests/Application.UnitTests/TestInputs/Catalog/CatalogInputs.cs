using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;

namespace TreniniDotNet.Application.TestInputs.Catalog
{
    public static class CatalogInputs
    {
        public static class NewBrandInput
        {
            public static CreateBrandInput Empty() => With();

            public static CreateBrandInput With(
                string Name = null,
                string CompanyName = null,
                string GroupName = null,
                string Description = null,
                string WebsiteUrl = null,
                string EmailAddress = null,
                string BrandType = null,
                AddressInput Address = null) => new CreateBrandInput(
                    Name,
                    CompanyName,
                    GroupName,
                    Description,
                    WebsiteUrl,
                    EmailAddress,
                    BrandType,
                    Address);
        }

        public static class NewAddressInput
        {
            public static AddressInput NewEmpty() => With();

            public static AddressInput With(
                    string Line1 = null,
                    string Line2 = null,
                    string City = null,
                    string Region = null,
                    string PostalCode = null,
                    string Country = null) => new AddressInput
                    {
                        Line1 = Line1,
                        Line2 = Line2,
                        City = City,
                        Region = Region,
                        PostalCode = PostalCode,
                        Country = Country
                    };
        }
    }
}