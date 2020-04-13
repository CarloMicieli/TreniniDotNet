using System;
using TreniniDotNet.Common.Addresses;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public interface IShop : IShopInfo
    {
        Uri? WebsiteUrl { get; }

        Address? Address { get; }
    }
}
