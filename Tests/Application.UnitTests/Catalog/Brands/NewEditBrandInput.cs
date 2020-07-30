using TreniniDotNet.Application.Catalog.Brands.EditBrand;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Brands
{
    public static class NewEditBrandInput
    {
        public static readonly EditBrandInput Empty = With();

        public static EditBrandInput With(
            Slug? brandSlug = null,
            string name = null,
            string companyName = null,
            string groupName = null,
            string description = null,
            string websiteUrl = null,
            string emailAddress = null,
            string brandType = null,
            AddressInput address = null) => new EditBrandInput(
            brandSlug ?? Slug.Empty,
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
