using System;
using System.Net.Mail;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.PhoneNumbers;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public interface IShopsFactory
    {
        IShop NewShop(string name,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            Address? address,
            PhoneNumber? phoneNumber);
    }
}
