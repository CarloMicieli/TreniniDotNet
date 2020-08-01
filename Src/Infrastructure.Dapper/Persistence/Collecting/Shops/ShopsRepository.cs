using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Shops
{
    public sealed class ShopsRepository : IShopsRepository
    {
        private readonly IDatabaseContext _dbContext;

        public ShopsRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<PaginatedResult<Shop>> GetAllAsync(Page page)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var results = await connection.QueryAsync<ShopDto>(
                GetShopsWithPaginationQuery,
                new { @skip = page.Start, @limit = page.Limit + 1 });

            return new PaginatedResult<Shop>(
                page,
                results.Select(ProjectToDomain).ToList());
        }

        public async Task<ShopId> AddAsync(Shop shop)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(InsertShopCommand, new
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
            });

            return shop.Id;
        }

        public Task UpdateAsync(Shop brand)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ShopId id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Shop aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(ShopId shopId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteScalarAsync<string>(
                GetShopExistsQuery,
                new { slug = slug.ToString() });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<Shop?> GetBySlugAsync(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<ShopDto>(
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
            throw new NotImplementedException();
            /*
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

            return _shopsFactory.NewShop(
                new ShopId(dto.shop_id),
                dto.name,
                Slug.Of(dto.slug),
                dto.website_url.ToUriOpt(),
                dto.mail_address.ToMailAddressOpt(),
                address,
                dto.phone_number.ToPhoneNumberOpt(),
                dto.created.ToUtc(),
                dto.last_modified.ToUtcOrDefault(),
                dto.version);
                */
        }

        #region [ Query / Commands ]

        private const string InsertShopCommand = @"INSERT INTO shops(shop_id, name, slug, website_url, phone_number, mail_address,
                address_line1, address_line2, address_city, address_region, address_postal_code, address_country,
                created, last_modified, version) 
            VALUES(@ShopId, @Name, @Slug, @WebsiteUrl, @PhoneNumber, @MailAddress,
                @Line1, @Line2, @City, @Region, @PostalCode, @Country,
                @Created, @LastModified, @Version);";

        private const string GetShopExistsQuery = @"SELECT slug FROM shops WHERE slug = @slug LIMIT 1;";

        private const string GetShopBySlugQuery = @"SELECT * FROM shops WHERE slug = @slug LIMIT 1;";

        private const string GetShopInfoBySlugQuery = @"SELECT shop_id, name, slug, created, last_modified, version FROM shops WHERE slug = @slug LIMIT 1;";

        private const string GetShopsWithPaginationQuery = @"SELECT * FROM shops ORDER BY name LIMIT @limit OFFSET @skip;";

        #endregion
    }
}
