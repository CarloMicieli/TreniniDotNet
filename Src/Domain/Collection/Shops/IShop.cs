using System;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.PhoneNumbers;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public interface IShop : IShopInfo
    {
        Uri? WebsiteUrl { get; }

        Address? Address { get; }

        PhoneNumber? PhoneNumber { get; }
    }
}
