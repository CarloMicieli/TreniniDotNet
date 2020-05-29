﻿using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.PhoneNumbers;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public interface IShopsFactory
    {
        IShop NewShop(string name,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            Address? address,
            PhoneNumber? phoneNumber);

        IShop NewShop(
            ShopId id,
            string name,
            Slug slug,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            Address? address,
            PhoneNumber? phoneNumber,
            Instant created,
            Instant? modified,
            int version);
    }
}