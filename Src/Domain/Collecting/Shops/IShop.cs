using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.PhoneNumbers;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public interface IShop : IShopInfo
    {
        Uri? WebsiteUrl { get; }

        MailAddress? EmailAddress { get; }

        Address? Address { get; }

        PhoneNumber? PhoneNumber { get; }

        Instant CreatedDate { get; }

        Instant? ModifiedDate { get; }

        int Version { get; }
    }
}
