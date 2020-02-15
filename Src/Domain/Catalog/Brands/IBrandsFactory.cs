using System;
using System.Net.Mail;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrandsFactory
    {
        IBrand NewBrand(
            string name,
            string? companyName, 
            Uri? websiteUrl, 
            MailAddress? emailAddress,
            BrandKind kind);

        IBrand NewBrand(
            BrandId brandId,
            string name,
            Slug slug,
            string? companyName,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            BrandKind kind);
    }
}
