using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class Brand : IBrand, IEquatable<Brand>
    {
        internal Brand(BrandId id,
            string name,
            Slug slug,
            string? companyName,
            string? groupName,
            string? description,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            BrandKind kind,
            Address? address,
            Instant lastModified,
            int version)
        {
            BrandId = id;
            Slug = slug;
            Name = name;
            WebsiteUrl = websiteUrl;
            EmailAddress = emailAddress;
            CompanyName = companyName;
            GroupName = groupName;
            Description = description;
            Address = address;
            Kind = kind;
            Version = version;
            LastModifiedAt = lastModified;
        }

        #region [ Properties ]
        public BrandId BrandId { get; }

        public Slug Slug { get; }

        public string Name { get; }

        public Uri? WebsiteUrl { get; }

        public MailAddress? EmailAddress { get; }

        public string? CompanyName { get; }

        public string? GroupName { get; }

        public string? Description { get; }

        public Address? Address { get; }

        public BrandKind Kind { get; }

        public Instant? LastModifiedAt { get; }

        public int? Version { get; }
        #endregion

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is Brand that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(Brand left, Brand right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(Brand left, Brand right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(Brand other)
        {
            return AreEquals(this, other);
        }

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

        public override string ToString()
        {
            return $"Brand({Name})";
        }

        #endregion

        public IBrandInfo ToBrandInfo()
        {
            return this;
        }
    }
}
