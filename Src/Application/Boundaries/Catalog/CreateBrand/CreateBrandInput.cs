using System;
using System.Net.Mail;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public sealed class CreateBrandInput : IUseCaseInput
    {
        private readonly string _name;
        private readonly string? _companyName;
        private readonly Uri? _websiteUrl;
        private readonly MailAddress? _emailAddress;
        private readonly BrandKind? _kind; 

        public CreateBrandInput(string name, string? companyName, Uri? websiteUrl, MailAddress? emailAddress, BrandKind? kind)
        {
            _name = name;
            _companyName = companyName;
            _websiteUrl = websiteUrl;
            _emailAddress = emailAddress;
            _kind = kind;
        }

        public string Name => _name;

        public string? CompanyName => _companyName;

        public Uri? WebsiteUrl => _websiteUrl;

        public MailAddress? EmailAddress => _emailAddress;

        public BrandKind? BrandKind => _kind;
    }
}
