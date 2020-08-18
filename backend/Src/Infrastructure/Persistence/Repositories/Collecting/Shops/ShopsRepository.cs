using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.PhoneNumbers;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Shops
{
    public sealed class ShopsRepository : IShopsRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<PaginatedResult<Shop>> GetShopsAsync(Page page)
        {
            var results = await _unitOfWork.QueryAsync<ShopDto>(
                GetShopsWithPaginationQuery,
                new { @skip = page.Start, @limit = page.Limit + 1 });

            return new PaginatedResult<Shop>(
                page,
                results.Select(ProjectToDomain).ToList());
        }

        public async Task<ShopId> AddAsync(Shop shop)
        {
            var _ = await _unitOfWork.ExecuteAsync(
                InsertShopCommand,
                ToValuesObject(shop));
            return shop.Id;
        }

        public Task UpdateAsync(Shop shop)
        {
            return _unitOfWork.ExecuteAsync(
                UpdateShopCommand,
                ToValuesObject(shop));
        }

        public Task DeleteAsync(ShopId id)
        {
            return _unitOfWork.ExecuteAsync(
                DeleteShopCommand, new { shop_id = id.ToGuid() });
        }

        public async Task<bool> ExistsAsync(ShopId shopId)
        {
            var result = await _unitOfWork.ExecuteScalarAsync<string>(
                GetShopExistsByIdQuery,
                new { shop_id = shopId.ToGuid() });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<bool> ExistsAsync(Slug slug)
        {
            var result = await _unitOfWork.ExecuteScalarAsync<string>(
                GetShopExistsQuery,
                new { slug = slug.ToString() });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<Shop?> GetBySlugAsync(Slug slug)
        {
            var result = await _unitOfWork.QueryFirstOrDefaultAsync<ShopDto>(
                GetShopBySlugQuery,
                new { @slug = slug.ToString() });

            if (result is null)
            {
                return null;
            }

            return ProjectToDomain(result);
        }

        public Task AddShopToFavouritesAsync(Owner owner, ShopId shopId) =>
            _unitOfWork.ExecuteAsync(InsertShopFavourite, new
            {
                owner = owner.Value,
                shop_id = shopId.ToGuid()
            });

        public Task RemoveFromFavouritesAsync(Owner owner, ShopId shopId) =>
            _unitOfWork.ExecuteAsync(RemoveShopFavourite, new
            {
                owner = owner.Value,
                shop_id = shopId.ToGuid()
            });

        public async Task<List<Shop>> GetFavouriteShopsAsync(Owner owner)
        {
            var results = await _unitOfWork.QueryAsync<ShopDto>(
                GetFavouriteShopsQuery,
                new { owner = owner.Value });
            return results.Select(ProjectToDomain).ToList();
        }

        private Shop ProjectToDomain(ShopDto dto)
        {
            Address? address = null;

            if (Address.TryCreate(
                dto.address_line1,
                dto.address_line2,
                dto.address_city,
                dto.address_region,
                dto.address_postal_code,
                dto.address_country,
                out var a))
            {
                address = a;
            }

            return new Shop(
                new ShopId(dto.shop_id),
                dto.name,
                dto.website_url.ToUriOpt(),
                dto.mail_address.ToMailAddressOpt(),
                address,
                dto.phone_number.ToPhoneNumberOpt(),
                dto.created.ToUtc(),
                dto.last_modified.ToUtcOrDefault(),
                dto.version);
        }

        private static ShopParam ToValuesObject(Shop shop) => new ShopParam
        {
            shop_id = shop.Id.ToGuid(),
            name = shop.Name,
            slug = shop.Slug.ToString(),
            website_url = shop.WebsiteUrl?.ToString(),
            phone_number = shop.PhoneNumber?.ToString(),
            mail_address = shop.EmailAddress?.ToString(),
            address_line1 = shop.Address?.Line1,
            address_line2 = shop.Address?.Line2,
            address_city = shop.Address?.City,
            address_region = shop.Address?.Region,
            address_postal_code = shop.Address?.PostalCode,
            address_country = shop.Address?.Country,
            created = shop.CreatedDate.ToDateTimeUtc(),
            last_modified = shop.ModifiedDate?.ToDateTimeUtc(),
            version = shop.Version
        };

        #region [ Query / Commands ]

        private const string InsertShopCommand = @"INSERT INTO shops(shop_id, name, slug, website_url, phone_number, mail_address,
                address_line1, address_line2, address_city, address_region, address_postal_code, address_country,
                created, last_modified, version) 
            VALUES(@shop_id, @name, @slug, @website_url, @phone_number, @mail_address,
                @address_line1, @address_line2, @address_city, @address_region, @address_postal_code, @address_country,
                @created, @last_modified, @version);";

        private const string UpdateShopCommand = @"UPDATE shops SET
                name = @name, slug = @slug, website_url = @website_url, 
                phone_number = @phone_number, mail_address = @mail_address,
                address_line1 = @address_line1, 
                address_line2 = @address_line2, 
                address_city = @address_city, address_region = @address_region, 
                address_postal_code = @address_postal_code, 
                address_country = @address_country,
                created = @created, last_modified = @last_modified, version = @version
            WHERE shop_id = @shop_id;";

        private const string GetShopExistsQuery = @"SELECT slug FROM shops WHERE slug = @slug LIMIT 1;";

        private const string GetShopExistsByIdQuery = @"SELECT slug FROM shops WHERE shop_id = @shop_id LIMIT 1;";

        private const string GetShopBySlugQuery = @"SELECT * FROM shops WHERE slug = @slug LIMIT 1;";

        private const string GetShopsWithPaginationQuery = @"SELECT * FROM shops ORDER BY name LIMIT @limit OFFSET @skip;";

        private const string DeleteShopCommand = @"DELETE FROM shops WHERE shop_id = @shop_id;";

        private const string GetFavouriteShopsQuery = @"SELECT s.* 
                FROM shops AS s 
                JOIN shop_favourites sf on s.shop_id = sf.shop_id
                WHERE owner = @owner
                ORDER BY s.name";

        private const string InsertShopFavourite =
            @"INSERT INTO shop_favourites(shop_id, owner) VALUES(@shop_id, @owner);";

        private const string RemoveShopFavourite =
            @"DELETE FROM shop_favourites WHERE shop_id = @shop_id AND owner = @owner;";

        #endregion
    }
}
