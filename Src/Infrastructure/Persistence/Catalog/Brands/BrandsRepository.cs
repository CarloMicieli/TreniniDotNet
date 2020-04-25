using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Pagination;
using Dapper;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public sealed class BrandsRepository : IBrandsRepository
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IBrandsFactory _brandsFactory;

        public BrandsRepository(IDatabaseContext dbContext, IBrandsFactory brandsFactory)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
            _brandsFactory = brandsFactory ??
                throw new ArgumentNullException(nameof(brandsFactory));
        }

        public async Task<BrandId> AddAsync(IBrand brand)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(InsertBrandCommand, new
            {
                brand.BrandId,
                brand.Name,
                brand.Slug,
                brand.CompanyName,
                brand.GroupName,
                brand.Description,
                brand.EmailAddress,
                brand.WebsiteUrl,
                brand.Kind,
                AddressLine1 = brand.Address?.Line1,
                AddressLine2 = brand.Address?.Line2,
                AddressCity = brand.Address?.City,
                AddressRegion = brand.Address?.Region,
                AddressPostalCode = brand.Address?.PostalCode,
                AddressCountry = brand.Address?.Country,
                Created = brand.CreatedDate.ToDateTimeUtc(),
                Modified = brand.ModifiedDate?.ToDateTimeUtc(),
                brand.Version
            });
            return brand.BrandId;
        }

        public async Task UpdateAsync(IBrand brand)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var _ = await connection.ExecuteAsync(UpdateBrandCommand, new
            {
                brand.BrandId,
                brand.Name,
                brand.Slug,
                brand.CompanyName,
                brand.GroupName,
                brand.Description,
                brand.EmailAddress,
                brand.WebsiteUrl,
                brand.Kind,
                AddressLine1 = brand.Address?.Line1,
                AddressLine2 = brand.Address?.Line2,
                AddressCity = brand.Address?.City,
                AddressRegion = brand.Address?.Region,
                AddressPostalCode = brand.Address?.PostalCode,
                AddressCountry = brand.Address?.Country,
                Modified = brand.ModifiedDate?.ToDateTimeUtc(),
                brand.Version
            });
        }

        public async Task<bool> ExistsAsync(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteScalarAsync<string>(
                GetBrandExistsQuery,
                new { slug = slug.ToString() });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<PaginatedResult<IBrand>> GetBrandsAsync(Page page)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var results = await connection.QueryAsync<BrandDto>(
                GetAllBrandsWithPaginationQuery,
                new { @skip = page.Start, @limit = page.Limit + 1 });

            return new PaginatedResult<IBrand>(
                page,
                results.Select(b => FromBrandDto(b)!).ToList());
        }

        public async Task<IBrand?> GetBySlugAsync(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<BrandDto>(
                GetBrandBySlugQuery,
                new { @slug = slug.ToString() });

            return FromBrandDto(result);
        }

        public async Task<IBrandInfo?> GetInfoBySlugAsync(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<BrandInfoDto?>(
                GetBrandInfoBySlugQuery,
                new { @slug = slug.ToString() });

            if (result is null)
            {
                return null;
            }

            return ProjectInfoToDomain(result);
        }

        #region [ Helper methods ]

        private IBrandInfo ProjectInfoToDomain(BrandInfoDto dto)
        {
            return new BrandInfo(dto.brand_id, dto.slug, dto.name);
        }

        private IBrand? FromBrandDto(BrandDto? dto)
        {
            if (dto is null)
            {
                return null;
            }

            return _brandsFactory.NewBrand(
                dto.brand_id,
                dto.name,
                dto.slug,
                dto.kind,
                dto.company_name,
                dto.group_name,
                dto.description,
                dto.website_url,
                dto.mail_address,
                null,
                dto.created,
                dto.last_modified,
                dto.version ?? 1);
        }

        #endregion

        #region [ Query / Commands ]

        private const string GetBrandBySlugQuery = @"SELECT * FROM brands WHERE slug = @slug LIMIT 1;";

        private const string GetBrandInfoBySlugQuery = @"SELECT brand_id, name, slug FROM brands WHERE slug = @slug LIMIT 1;";

        private const string GetAllBrandsWithPaginationQuery = @"SELECT * FROM brands ORDER BY name LIMIT @limit OFFSET @skip;";

        private const string GetBrandExistsQuery = @"SELECT slug FROM brands WHERE slug = @slug LIMIT 1;";

        private const string InsertBrandCommand = @"INSERT INTO brands(
                brand_id, name, slug, company_name, group_name, description, 
                address_line1, address_line2, address_city, address_region, address_postal_code, address_country,
                mail_address, website_url, kind, created, last_modified, version)
            VALUES(
                @BrandId, @Name, @Slug, @CompanyName, @GroupName, @Description, 
                @AddressLine1, @AddressLine2, @AddressCity, @AddressRegion, @AddressPostalCode, @AddressCountry,
                @EmailAddress, @WebsiteUrl, @Kind, @Created, @Modified, @Version);";

        private const string UpdateBrandCommand = @"UPDATE brands SET
                name = @Name, slug = @Slug, company_name = @CompanyName, group_name = @GroupName, 
                description = @Description, 
                address_line1 = @AddressLine1, address_line2 = @AddressLine2, address_city = @AddressCity, 
                address_region = @AddressRegion, address_postal_code = @AddressPostalCode, address_country = @AddressCountry,
                mail_address = @EmailAddress, website_url = @WebsiteUrl, kind = @Kind, 
                last_modified = @Modified, 
                version = @Version
            WHERE brand_id = @BrandId;";

        #endregion
    }
}
