using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.PhoneNumbers;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Shops
{
    internal class FakeShop : IShop
    {
        public Uri WebsiteUrl { get; set; }

        public MailAddress EmailAddress { get; set; }

        public Address Address { get; set; }

        public PhoneNumber? PhoneNumber { get; set; }

        public ShopId ShopId { get; set; }

        public Slug Slug { get; set; }

        public string Name { get; set; }

        public Instant CreatedDate { get; set; }

        public Instant? ModifiedDate { get; set; }

        public int Version { get; set; }

        public FakeShop()
        {
            ShopId = new ShopId(new Guid("dcba5221-cb2b-4961-976d-c6df34c2c6db"));
            Name = "Test Shop";
            Slug = Slug.Of("Test Shop");

            WebsiteUrl = new Uri("https://www.testshop.com");
            EmailAddress = new MailAddress("mail@mail.com");
            Address = null;
            PhoneNumber = TreniniDotNet.Common.PhoneNumbers.PhoneNumber.Of("+39 555 123456");

            CreatedDate = Instant.FromUtc(2019, 11, 25, 9, 0);
            ModifiedDate = null;
            Version = 1;
        }
    }
}
