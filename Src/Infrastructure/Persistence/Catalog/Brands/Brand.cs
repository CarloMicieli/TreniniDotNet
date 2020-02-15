using System;
using System.Net.Mail;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public class Brand : IBrand
    {
        public BrandId BrandId { set; get; }

        public Slug Slug { set; get; }

        public string Name { set; get; } = null!;

        public Uri? WebsiteUrl { set; get; }

        public MailAddress? EmailAddress { set; get; }

        public string? CompanyName { set; get; }

        public BrandKind Kind { set; get; }
    }
}
