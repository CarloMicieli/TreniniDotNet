using System;
using System.Net.Mail;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.Entities;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrand : IBrandInfo, IModifiableEntity
    {
        Uri? WebsiteUrl { get; }

        MailAddress? EmailAddress { get; }

        string? CompanyName { get; }

        string? GroupName { get; }

        string? Description { get; }

        BrandKind Kind { get; }

        Address? Address { get; }

        IBrandInfo ToBrandInfo();

        IBrand With(
            string? companyName = null,
            string? groupName = null,
            string? description = null,
            Uri? websiteUrl = null,
            MailAddress? mailAddress = null,
            Address? address = null,
            BrandKind? kind = null) => new Brand(
                BrandId,
                Name,
                Slug,
                companyName ?? CompanyName,
                groupName ?? GroupName,
                description ?? Description,
                websiteUrl ?? WebsiteUrl,
                mailAddress ?? EmailAddress,
                kind ?? Kind,
                address ?? Address,
                CreatedDate,
                ModifiedDate,
                Version);
    }
}
