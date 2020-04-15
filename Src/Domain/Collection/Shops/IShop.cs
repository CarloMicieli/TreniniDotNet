using System;
using System.Net.Mail;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.PhoneNumbers;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public interface IShop : IShopInfo
    {
        Uri? WebsiteUrl { get; }

        MailAddress? EmailAddress { get; }

        Address? Address { get; }

        PhoneNumber? PhoneNumber { get; }
    }
}
