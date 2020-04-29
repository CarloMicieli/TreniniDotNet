using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.PhoneNumbers;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public sealed class Shop : IShop, IEquatable<Shop>
    {
        internal Shop(ShopId shopId, Slug slug, string name,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            Address? address,
            PhoneNumber? phoneNumber,
            Instant createdDate,
            Instant? modifiedDate,
            int version)
        {
            ShopId = shopId;
            Slug = slug;
            Name = name;
            WebsiteUrl = websiteUrl;
            EmailAddress = emailAddress;
            Address = address;
            PhoneNumber = phoneNumber;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            Version = version;
        }

        public Uri? WebsiteUrl { get; }

        public MailAddress? EmailAddress { get; }

        public Address? Address { get; }

        public PhoneNumber? PhoneNumber { get; }

        public ShopId ShopId { get; }

        public Slug Slug { get; }

        public string Name { get; }

        public Instant CreatedDate { get; }

        public Instant? ModifiedDate { get; }

        public int Version { get; }

        #region [ Equality ]

        public static bool operator ==(Shop left, Shop right) => AreEquals(left, right);

        public static bool operator !=(Shop left, Shop right) => !AreEquals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is Shop that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(Shop other) => AreEquals(this, other);

        private static bool AreEquals(Shop left, Shop right) =>
            left.ShopId == right.ShopId &&
            left.Name == right.Name &&
            left.Slug == right.Slug;

        #endregion

        public override string ToString() =>
            $"Shop({ShopId}, {Name}, {Slug})";

        public override int GetHashCode() =>
            HashCode.Combine(ShopId, Name, Slug);
    }
}
