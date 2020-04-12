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
    }
}
