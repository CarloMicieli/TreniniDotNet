using System;
using System.Net.Mail;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class BrandsFactory : IBrandsFactory
    {
        public IBrand NewBrand(string name, string? companyName, Uri? websiteUrl, MailAddress? emailAddress, BrandKind kind)
        {
            throw new NotImplementedException();
        }

        public IBrand NewBrand(string name, string? companyName, string? websiteUrl, string? emailAddress, BrandKind kind)
        {
            throw new NotImplementedException();
        }

        public IBrand NewBrand(BrandId brandId, string name, Slug slug, string? companyName, Uri? websiteUrl, MailAddress? emailAddress, BrandKind kind)
        {
            throw new NotImplementedException();
        }
    }
}
