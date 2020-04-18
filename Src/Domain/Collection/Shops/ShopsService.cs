using System;
using System.Net.Mail;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.PhoneNumbers;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public sealed class ShopsService
    {
        private readonly IShopsRepository _shops;
        private readonly IShopsFactory _factory;

        public ShopsService(IShopsRepository shopsRepository, IShopsFactory factory)
        {
            _shops = shopsRepository ??
                throw new ArgumentNullException(nameof(shopsRepository));
            _factory = factory ??
                throw new ArgumentNullException(nameof(factory));
        }

        public Task<bool> ExistsAsync(Slug slug) =>
            _shops.ExistsAsync(slug);

        public Task<ShopId> CreateShop(
            string name,
            Uri? websiteUrl,
            MailAddress? mailAddress,
            Address? address,
            PhoneNumber? phoneNumber)
        {
            var newShop = _factory.NewShop(
                name,
                websiteUrl,
                mailAddress,
                address,
                phoneNumber);

            return _shops.AddAsync(newShop);
        }
    }
}
