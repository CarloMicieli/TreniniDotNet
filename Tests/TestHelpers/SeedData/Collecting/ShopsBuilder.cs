using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.PhoneNumbers;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.TestHelpers.SeedData.Collecting
{
    public sealed class ShopsBuilder
    {
        private ShopId _shopId;
        private string _name;
        private Uri _websiteUrl;
        private MailAddress _emailAddress;
        private Address _address;
        private PhoneNumber? _phoneNumber;
        private Instant _createdDate;
        private Instant? _modifiedDate;
        private int _version;

        internal ShopsBuilder()
        {
            _shopId = ShopId.NewId();
            _createdDate = Instant.FromDateTimeUtc(DateTime.UtcNow);
            _modifiedDate = null;
            _version = 1;
        }

        public Shop Build()
        {
            return new Shop(
                _shopId,
                _name,
                _websiteUrl,
                _emailAddress,
                _address,
                _phoneNumber,
                _createdDate,
                _modifiedDate,
                _version);
        }

        public ShopsBuilder Id(Guid id)
        {
            _shopId = new ShopId(id);
            return this;
        }

        public ShopsBuilder Name(string name)
        {
            _name = name;
            return this;
        }

        public ShopsBuilder WebsiteUrl(Uri websiteUrl)
        {
            _websiteUrl = websiteUrl;
            return this;
        }

        public ShopsBuilder MailAddress(MailAddress mailAddress)
        {
            _emailAddress = mailAddress;
            return this;
        }

        public ShopsBuilder Address(Address address)
        {
            _address = address;
            return this;
        }

        public ShopsBuilder PhoneNumber(PhoneNumber phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }
    }
}
