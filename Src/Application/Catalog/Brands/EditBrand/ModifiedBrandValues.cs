namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
{
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
