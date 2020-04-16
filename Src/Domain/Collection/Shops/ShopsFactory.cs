using NodaTime;
using System;
using System.Net.Mail;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.PhoneNumbers;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public sealed class ShopsFactory : IShopsFactory
    {
        private readonly IClock _clock;
        private readonly IGuidSource _guidSource;

        public ShopsFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock ??
                throw new ArgumentNullException(nameof(clock));

            _guidSource = guidSource ??
                throw new ArgumentNullException(nameof(guidSource));
        }

        public IShop NewShop(string name,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            Address? address,
            PhoneNumber? phoneNumber) =>
            new Shop(
                new ShopId(_guidSource.NewGuid()),
                Slug.Of(name),
                name,
                websiteUrl,
                emailAddress,
                address,
                phoneNumber,
                _clock.GetCurrentInstant(),
                null,
                1);
    }
}
