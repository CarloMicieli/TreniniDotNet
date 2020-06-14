using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.PhoneNumbers;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public sealed class Shop : AggregateRoot<ShopId>, IShop
    {
        internal Shop(
            ShopId shopId,
            Slug slug, string name,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            Address? address,
            PhoneNumber? phoneNumber,
            Instant createdDate,
            Instant? modifiedDate,
            int version)
            : base(shopId, createdDate, modifiedDate, version)
        {
            Slug = slug;
            Name = name;
            WebsiteUrl = websiteUrl;
            EmailAddress = emailAddress;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public Uri? WebsiteUrl { get; }

        public MailAddress? EmailAddress { get; }

        public Address? Address { get; }

        public PhoneNumber? PhoneNumber { get; }

        public Slug Slug { get; }

        public string Name { get; }

        public override string ToString() =>
            $"Shop({Id}, {Name}, {Slug})";
    }
}
