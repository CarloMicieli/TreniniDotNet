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

        Instant? LastModifiedAt { get; }

        int? Version { get; }

        IBrandInfo ToBrandInfo();
    }
}
