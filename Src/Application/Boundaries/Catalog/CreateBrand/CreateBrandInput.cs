using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public sealed class CreateBrandInput : IUseCaseInput
    {
        public CreateBrandInput(string? name, string? companyName, string? websiteUrl, string? emailAddress, string? kind)
        {
            Name = name;
            CompanyName = companyName;
            WebsiteUrl = websiteUrl;
            EmailAddress = emailAddress;
            Kind = kind;
        }

        public string? Name { get; }

        public string? CompanyName { get; }

        public string? GroupName { get; } = null;

        public string? Description { get; } = null;

        public string? WebsiteUrl { get; }

        public string? EmailAddress { get; }

        public string? Kind { get; }

        public AddressInput? Address { get; } = null; //TODO fixme
    }

    public sealed class AddressInput
    {
        public string? Line1 { set; get; }
        public string? Line2 { set; get; }
        public string? City { set; get; }
        public string? Region { set; get; }
        public string? PostalCode { set; get; }
        public string? Country { set; get; }
    }
}
