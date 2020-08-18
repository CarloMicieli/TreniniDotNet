using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class Brand : AggregateRoot<BrandId>
    {
        public Brand(BrandId brandId, Slug slug, string name,
            Uri? websiteUrl, MailAddress? emailAddress,
            string? companyName, string? groupName, string? description,
            Address? address, BrandKind kind,
            Instant created, Instant? lastModified, int version)
            : base(brandId, created, lastModified, version)
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

        public Brand With(
            string? name = null,
            BrandKind? brandKind = null,
            string? companyName = null,
            string? groupName = null,
            string? description = null,
            Uri? websiteUrl = null,
            MailAddress? mailAddress = null,
            Address? address = null)
        {
            var slug = (name is null) ? Slug : Slug.Of(name);
            return new Brand(
                Id,
                slug,
                name ?? Name,
                websiteUrl ?? WebsiteUrl,
                mailAddress ?? EmailAddress,
                companyName ?? CompanyName,
                groupName ?? GroupName,
                description ?? Description,
                address ?? Address,
                brandKind ?? Kind,
                CreatedDate,
                ModifiedDate,
                Version);
        }

        public override string ToString() => $"Brand({Name})";
    }
}
