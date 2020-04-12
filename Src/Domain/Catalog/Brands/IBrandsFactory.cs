using System;
using System.Net.Mail;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrandsFactory
    {
        IBrand NewBrand(
            string name,
            string? companyName,
            string? groupName,
            string? description,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            BrandKind kind,
            Address? address);

        IBrand NewBrand(Guid brandId,
            string name, string slug,
            string kind,
            string? companyName, string? groupName,
            string? description,
            string? websiteUrl,
            string? mailAddress,
            Address? address,
            DateTime created,
            DateTime? modified,
            int version);

        IBrand NewBrandWith(
            BrandId brandId,
            string name,
            Slug? slug = null,
            BrandKind? kind = null,
            string? companyName = null,
            string? groupName = null,
            string? description = null,
            Uri? website = null,
            MailAddress? mailAddress = null,
            Address? address = null);
    }
}
