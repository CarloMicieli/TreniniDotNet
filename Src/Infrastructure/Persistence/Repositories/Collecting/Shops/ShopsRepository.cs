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
                DeleteShopCommand, new {Id = id.ToGuid()});
        }

        public async Task<bool> ExistsAsync(ShopId shopId)
        {
            var result = await _unitOfWork.ExecuteScalarAsync<string>(
                GetShopExistsByIdQuery,
                new { Id = shopId.ToGuid() });

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

        public Task AddShopToFavouritesAsync(Owner owner, ShopId shopId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromFavouritesAsync(Owner owner, ShopId shopId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Shop>> GetFavouriteShopsAsync(Owner owner)
        {
            throw new NotImplementedException();
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

        private static object ToValuesObject(Shop shop) => new
        {
            ShopId = shop.Id,
            shop.Name,
            Slug = shop.Slug.ToString(),
            WebsiteUrl = shop.WebsiteUrl?.ToString(),
            PhoneNumber = shop.PhoneNumber?.ToString(),
            MailAddress = shop.EmailAddress?.ToString(),
            shop.Address?.Line1,
            shop.Address?.Line2,
            shop.Address?.City,
            shop.Address?.Region,
            shop.Address?.PostalCode,
            shop.Address?.Country,
            Created = shop.CreatedDate.ToDateTimeUtc(),
            LastModified = shop.ModifiedDate?.ToDateTimeUtc(),
            shop.Version
        };
        
        #region [ Query / Commands ]

        private const string InsertShopCommand = @"INSERT INTO shops(shop_id, name, slug, website_url, phone_number, mail_address,
                address_line1, address_line2, address_city, address_region, address_postal_code, address_country,
                created, last_modified, version) 
            VALUES(@ShopId, @Name, @Slug, @WebsiteUrl, @PhoneNumber, @MailAddress,
                @Line1, @Line2, @City, @Region, @PostalCode, @Country,
                @Created, @LastModified, @Version);";

        private const string UpdateShopCommand = @"UPDATE shops SET
                name = @Ņame, slug = @Slug, website_url = @WebsiteUrl, 
                phone_number = @PhoneNumber, mail_address = @MailAddress,
                address_line1 = @Line1, 
                address_line2 = @Line2, 
                address_city = @City, address_region = @Region, address_postal_code = @PostalCode, 
                address_country = @Country,
                created = @Created, last_modified = @LastModified, version = @Version
            WHERE shop_id = @Id;";

        private const string GetShopExistsQuery = @"SELECT slug FROM shops WHERE slug = @slug LIMIT 1;";
        
        private const string GetShopExistsByIdQuery = @"SELECT slug FROM shops WHERE shop_id = @Id LIMIT 1;";

        private const string GetShopBySlugQuery = @"SELECT * FROM shops WHERE slug = @slug LIMIT 1;";

        private const string GetShopsWithPaginationQuery = @"SELECT * FROM shops ORDER BY name LIMIT @limit OFFSET @skip;";

        private const string DeleteShopCommand = @"DELETE FROM shops WHERE shop_id = @Id";

        #endregion
    }
}
