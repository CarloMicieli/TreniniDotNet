using TreniniDotNet.Application.Boundaries.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditBrand
{
    public sealed class EditBrandInput : IUseCaseInput
    {
        public EditBrandInput(
            string? name,
            string? slug,
            string? companyName,
            string? groupName,
            string? description,
            string? websiteUrl,
            string? emailAddress,
            string? brandType,
            AddressInput? address)
        {
            Name = name;
            Slug = slug;
            CompanyName = companyName;
            GroupName = groupName;
            Description = description;
            WebsiteUrl = websiteUrl;
            EmailAddress = emailAddress;
            BrandType = brandType;
            Address = address;
        }

        public string? Name { get; }
        public string? Slug { get; }
        public string? CompanyName { get; }
        public string? GroupName { get; }
        public string? Description { get; }
        public string? WebsiteUrl { get; }
        public string? EmailAddress { get; }
        public string? BrandType { get; }
        public AddressInput? Address { get; }
    }
}
