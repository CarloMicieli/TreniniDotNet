using System;
using System.Net.Mail;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.Entities;
using TreniniDotNet.Common.PhoneNumbers;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public interface IShop : IShopInfo, IModifiableEntity
    {
        Uri? WebsiteUrl { get; }

        MailAddress? EmailAddress { get; }

        Address? Address { get; }

        PhoneNumber? PhoneNumber { get; }
    }
}
