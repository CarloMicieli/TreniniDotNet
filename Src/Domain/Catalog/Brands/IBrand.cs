using System;
using System.Net.Mail;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrand : IBrandInfo
    {
        Uri? WebsiteUrl { get; }

        MailAddress? EmailAddress { get; }

        string? CompanyName { get; }

        BrandKind Kind { get; }

        DateTime? CreatedAt { get; }

        int? Version { get; }

        IBrandInfo ToBrandInfo();
    }
}
