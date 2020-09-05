using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.PhoneNumbers;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public sealed class ShopsFactory : AggregateRootFactory<ShopId, Shop>
    {
        public ShopsFactory(IClock clock, IGuidSource guidSource)
            : base(clock, guidSource)
        {
        }

        public Shop CreateShop(
            string name,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            Address? address,
            PhoneNumber? phoneNumber)
        {
            return new Shop(
                NewId(id => new ShopId(id)),
                name,
                websiteUrl,
                emailAddress,
                address,
                phoneNumber,
                GetCurrentInstant(),
                null,
                1);
        }
    }
}
