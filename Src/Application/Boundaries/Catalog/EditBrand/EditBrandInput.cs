using TreniniDotNet.Application.Boundaries.Common;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditBrand
{
    public sealed class EditBrandInput : IUseCaseInput
    {
        public EditBrandInput(
            Slug brandSlug,
            string? name,
            string? companyName,
            string? groupName,
            string? description,
            string? websiteUrl,
            string? emailAddress,
            string? brandType,
            AddressInput? address)
        {
            BrandSlug = brandSlug;
            Values = new ModifiedBrandValues(
                name,
                companyName,
                groupName,
                description,
                websiteUrl,
                emailAddress,
                brandType,
                address);
        }

        public Slug BrandSlug { get; }
        public ModifiedBrandValues Values { get; }
    }

    public sealed class ModifiedBrandValues
    {
        public ModifiedBrandValues(
            string? name,
            string? companyName,
            string? groupName,
            string? description,
            string? websiteUrl,
            string? emailAddress,
            string? brandType,
            AddressInput? address)
        {
            Name = name;
            CompanyName = companyName;
            GroupName = groupName;
            Description = description;
            WebsiteUrl = websiteUrl;
            EmailAddress = emailAddress;
            BrandType = brandType;
            Address = address;
        }

        public string? Name { get; }
        public string? CompanyName { get; }
        public string? GroupName { get; }
        public string? Description { get; }
        public string? WebsiteUrl { get; }
        public string? EmailAddress { get; }
        public string? BrandType { get; }
        public AddressInput? Address { get; }
    }
}
