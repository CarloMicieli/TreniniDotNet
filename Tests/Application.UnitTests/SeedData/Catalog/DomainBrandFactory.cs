using System;
using System.Net.Mail;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.SeedData.Catalog
{
    class DomainBrandFactory : IBrandsFactory
    {
        public IBrand NewBrand(string name, string companyName, Uri websiteUrl, MailAddress emailAddress, BrandKind kind)
        {
            return new Brand(name,
                companyName,
                websiteUrl,
                emailAddress,
                kind);
        }

        public IBrand NewBrand(BrandId brandId, string name, Slug slug, string companyName, Uri websiteUrl, MailAddress emailAddress, BrandKind kind)
        {
            return new Brand(
                brandId,
                name,
                slug,
                companyName,
                websiteUrl,
                emailAddress,
                kind);
        }
    }
}
