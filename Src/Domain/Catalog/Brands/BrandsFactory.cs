using System;
using System.Net.Mail;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class BrandsFactory : IBrandsFactory
    {
        public IBrand NewBrand(Guid brandId, string name, string slug, string? companyName, string? websiteUrl, string? emailAddress, string? brandKind)
        {
            return new Brand(
                id: new BrandId(brandId),
                name: name,
                slug: Slug.Of(slug),
                companyName: companyName,
                websiteUrl: !string.IsNullOrWhiteSpace(websiteUrl) ? new Uri(websiteUrl) : null,
                emailAddress: !string.IsNullOrWhiteSpace(emailAddress) ? new MailAddress(emailAddress) : null,
                kind: brandKind.ToBrandKind()
                );
        }

        public IBrand NewBrand(BrandId brandId, string name, Slug slug, string? companyName, Uri? websiteUrl, MailAddress? mailAddress, BrandKind? kind)
        {
            return new Brand(
                id: brandId,
                name: name,
                slug: slug,
                companyName: companyName,
                websiteUrl: websiteUrl,
                emailAddress: mailAddress,
                kind: kind ?? BrandKind.Industrial
                );
        }
    }
}
