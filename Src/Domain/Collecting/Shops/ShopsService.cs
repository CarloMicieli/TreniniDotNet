using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.PhoneNumbers;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public sealed class ShopsService : IDomainService
    {
        private readonly IShopsRepository _shopsRepository;
        private readonly ShopsFactory _shopsFactory;

        public ShopsService(ShopsFactory shopsFactory, IShopsRepository shopsRepository)
        {
            _shopsFactory = shopsFactory ?? throw new ArgumentNullException(nameof(shopsFactory));
            _shopsRepository = shopsRepository ?? throw new ArgumentNullException(nameof(shopsRepository));
        }

        public Task<bool> ExistsAsync(ShopId shopId) => _shopsRepository.ExistsAsync(shopId);

        public Task<bool> ExistsAsync(Slug slug) =>
            _shopsRepository.ExistsAsync(slug);

        public Task<ShopId> CreateShopAsync(
            string name,
            Uri? websiteUrl,
            MailAddress? mailAddress,
            Address? address,
            PhoneNumber? phoneNumber)
        {
            var shop = _shopsFactory.CreateShop(
                name,
                websiteUrl,
                mailAddress,
                address,
                phoneNumber);

            return _shopsRepository.AddAsync(shop);
        }

        public Task<Shop?> GetBySlugAsync(Slug slug) =>
            _shopsRepository.GetBySlugAsync(slug);

        public Task<PaginatedResult<Shop>> GetAllShopsAsync(Page page) =>
            _shopsRepository.GetShopsAsync(page);

        public Task AddShopToFavouritesAsync(Owner owner, ShopId shopId) =>
            _shopsRepository.AddShopToFavouritesAsync(owner, shopId);

        public Task RemoveFromFavouritesAsync(Owner owner, ShopId shopId) =>
            _shopsRepository.RemoveFromFavouritesAsync(owner, shopId);

        public Task<List<Shop>> GetFavouriteShops(Owner owner) =>
            _shopsRepository.GetFavouriteShopsAsync(owner);
    }
}
