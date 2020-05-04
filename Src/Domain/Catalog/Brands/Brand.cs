using System;
using System.Runtime.CompilerServices;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.Entities;
using TreniniDotNet.Domain.Catalog.ValueObjects;

[assembly: InternalsVisibleTo("TestHelpers")]
namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class Brand : ModifiableEntity, IBrand, IEquatable<Brand>
    {
        internal Brand(BrandId brandId,
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
            : base(created, modified, version)
        {
            BrandId = brandId;
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
        public BrandId BrandId { get; }

        public Slug Slug { get; }

        public string Name { get; } = null!;

        public Uri? WebsiteUrl { get; }

        public MailAddress? EmailAddress { get; }

        public string? CompanyName { get; }

        public string? GroupName { get; }

        public string? Description { get; }

        public Address? Address { get; }

        public BrandKind Kind { get; }
        #endregion

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is Brand that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(Brand other) => AreEquals(this, other);

        private static bool AreEquals(Brand left, Brand right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            return left.BrandId == right.BrandId &&
                left.Name == right.Name;
        }
        #endregion

        #region [ Standard methods overrides ]

        public override int GetHashCode() =>
            HashCode.Combine(BrandId, Name);

        public override string ToString() => $"Brand({Name})";

        #endregion

        public IBrandInfo ToBrandInfo() => this;
    }
}
