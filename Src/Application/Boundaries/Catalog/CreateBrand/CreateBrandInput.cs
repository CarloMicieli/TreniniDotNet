using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public sealed class CreateBrandInput : IUseCaseInput
    {
        public string Name { get; }
        public string? CompanyName { get; }
        public string? GroupName { get; }
        public string? Description { get; }
        public string? WebsiteUrl { get; }
        public string? EmailAddress { get; }
        public string? Kind { get; }
        public AddressInput? Address { get; }

        public CreateBrandInput(
            string? name,
            string? companyName,
            string? groupName,
            string? description,
            string? websiteUrl,
            string? emailAddress,
            string? brandType,
            AddressInput? address)
        {
            Name = name!;
            CompanyName = companyName;
            GroupName = groupName;
            Description = description;
            WebsiteUrl = websiteUrl;
            EmailAddress = emailAddress;
            Kind = brandType;
            Address = address;
        }
    }
}
