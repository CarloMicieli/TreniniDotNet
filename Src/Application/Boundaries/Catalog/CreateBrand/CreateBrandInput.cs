using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public sealed class CreateBrandInput : IUseCaseInput
    {
        private readonly string? _name;
        private readonly string? _companyName;
        private readonly string? _websiteUrl;
        private readonly string? _emailAddress;
        private readonly string? _kind;

        public CreateBrandInput(string? name, string? companyName, string? websiteUrl, string? emailAddress, string? kind)
        {
            _name = name;
            _companyName = companyName;
            _websiteUrl = websiteUrl;
            _emailAddress = emailAddress;
            _kind = kind;
        }

        public string? Name => _name;

        public string? CompanyName => _companyName;

        public string? WebsiteUrl => _websiteUrl;

        public string? EmailAddress => _emailAddress;

        public string? Kind => _kind;
    }
}
