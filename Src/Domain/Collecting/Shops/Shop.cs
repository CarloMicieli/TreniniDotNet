using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.PhoneNumbers;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public sealed class Shop : AggregateRoot<ShopId>
    {
        public Shop(
            ShopId shopId,
            string name,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            Address? address,
            PhoneNumber? phoneNumber,
            Instant createdDate,
            Instant? modifiedDate,
            int version)
            : base(shopId, createdDate, modifiedDate, version)
        {
            Name = name;
            WebsiteUrl = websiteUrl;
            EmailAddress = emailAddress;
            Address = address;
            PhoneNumber = phoneNumber;
            Slug = Slug.Of(name);
        }

        #region [ Properties ]
        public string Name { get; }

        public Slug Slug { get; }

        public Uri? WebsiteUrl { get; }

        public MailAddress? EmailAddress { get; }

        public Address? Address { get; }

        public PhoneNumber? PhoneNumber { get; }

        #endregion

        public override string ToString() =>
            $"Shop({Id}, {Name}, {Slug})";
    }
}
