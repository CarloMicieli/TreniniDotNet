using TreniniDotNet.Application.Catalog.Brands.CreateBrand;

namespace TreniniDotNet.Application.Catalog.Brands
{
    public static class NewCreateBrandInput
    {
        public static readonly CreateBrandInput Empty = With();

        public static CreateBrandInput With(
            string name = null,
            string companyName = null,
            string groupName = null,
            string description = null,
            string websiteUrl = null,
            string emailAddress = null,
            string brandType = null,
            AddressInput address = null) => new CreateBrandInput(
            name,
            companyName,
            groupName,
            description,
            websiteUrl,
            emailAddress,
            brandType,
            address);
    }
}
