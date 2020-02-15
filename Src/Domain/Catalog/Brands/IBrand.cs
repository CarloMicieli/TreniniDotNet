using System;
using System.Net.Mail;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrand
    {
        BrandId BrandId { get; }

        Slug Slug { get; }

        string Name { get; }

        Uri? WebsiteUrl { get; }

        MailAddress? EmailAddress { get; }

        string? CompanyName { get; }

        BrandKind Kind { get; }
    }
}
