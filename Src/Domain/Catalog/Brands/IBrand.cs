using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common.Addresses;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrand : IBrandInfo
    {
        Uri? WebsiteUrl { get; }

        MailAddress? EmailAddress { get; }

        string? CompanyName { get; }

        string? GroupName { get; }

        string? Description { get; }

        BrandKind Kind { get; }

        Address? Address { get; }

        Instant CreatedDate { get; }

        Instant? ModifiedDate { get; }

        int Version { get; }

        IBrandInfo ToBrandInfo();

        IBrand With(
            string? companyName = null,
            string? groupName = null,
            string? description = null,
            Uri? websiteUrl = null,
            MailAddress? mailAddress = null,
            Address? address = null,
            BrandKind? kind = null) => new Brand(
                Id,
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
