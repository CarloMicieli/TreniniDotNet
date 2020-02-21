using System;
using System.Net.Mail;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public sealed class BrandsFactory : IBrandsFactory
    {
        public IBrand NewBrand(string name, string? companyName, Uri? websiteUrl, MailAddress? emailAddress, BrandKind kind)
        {
            return new Brand
            {
                BrandId = BrandId.NewId(),
                Name = name,
                Slug = Slug.Of(name),
                CompanyName = companyName,
                WebsiteUrl = websiteUrl,
                EmailAddress = emailAddress,
                Kind = kind
            };
        }

        public IBrand NewBrand(string name, string? companyName, string? websiteUrl, string? emailAddress, BrandKind kind)
        {
            return new Brand
            {
                BrandId = BrandId.NewId(),
                Name = name,
                Slug = Slug.Of(name),
                CompanyName = companyName,
                WebsiteUrl = websiteUrl != null ? new Uri(websiteUrl) : null,
                EmailAddress = emailAddress != null ? new MailAddress(emailAddress) : null,
                Kind = kind
            };
        }

        public IBrand NewBrand(BrandId brandId, string name, Slug slug, string? companyName, Uri? websiteUrl, MailAddress? emailAddress, BrandKind kind)
        {
            return new Brand
            {
                BrandId = brandId,
                Name = name,
                Slug = slug,
                CompanyName = companyName,
                WebsiteUrl = websiteUrl,
                EmailAddress = emailAddress,
                Kind = kind
            };
        }
    }
}
