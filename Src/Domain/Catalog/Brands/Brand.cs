using System;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Domain.Catalog.ValueObjects;

[assembly: InternalsVisibleTo("TestHelpers")]
namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class Brand : AggregateRoot<BrandId>, IBrand
    {
        internal Brand(
            BrandId brandId,
            string name,
            Slug slug,
            string? companyName,
            string? groupName,
            string? description,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            BrandKind kind,
            Address? address,
            Instant created,
            Instant? modified,
            int version)
            : base(brandId, created, modified, version)
        {
            Slug = slug;
            Name = name;
            WebsiteUrl = websiteUrl;
            EmailAddress = emailAddress;
            CompanyName = companyName;
            GroupName = groupName;
            Description = description;
            Address = address;
            Kind = kind;
        }

        #region [ Properties ]
        public Slug Slug { get; }

        public string Name { get; }

        public Uri? WebsiteUrl { get; }

        public MailAddress? EmailAddress { get; }

        public string? CompanyName { get; }

        public string? GroupName { get; }

        public string? Description { get; }

        public Address? Address { get; }

        public BrandKind Kind { get; }
        #endregion

        public override string ToString() => $"Brand({Name})";

        public IBrandInfo ToBrandInfo() => this;
    }
}
