using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System.Collections.Generic;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Infrastracture.Dapper;
using Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public sealed class BrandsRepository : IBrandsRepository
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IBrandsFactory _brandsFactory;

        public BrandsRepository(IDatabaseContext dbContext, IBrandsFactory brandsFactory)
        {
            _dbContext = dbContext;
            _brandsFactory = brandsFactory;
        }

        public async Task<BrandId> Add(IBrand brand)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(InsertBrandCommand, brand);
            return brand.BrandId;
        }

        public async Task<bool> Exists(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteScalarAsync<string>(
                GetBrandExistsQuery,
                new { slug = slug.ToString() });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<List<IBrand>> GetAll()
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryAsync<BrandDto>(
                GetAllBrandsQuery,
                new { });

            if (result is null)
            {
                return new List<IBrand>();
            }

            return result.Select(b => FromBrandDto(b)!).ToList();
        }

        public async Task<PaginatedResult<IBrand>> GetBrands(Page page)
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

        public async Task<IBrand?> GetByName(string name)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<BrandDto>(
                GetBrandByNameQuery,
                new { name });

            return FromBrandDto(result);
        }

        public async Task<IBrand?> GetBySlug(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<BrandDto>(
                GetBrandBySlugQuery,
                new { @slug = slug.ToString() });

            return FromBrandDto(result);
        }

        #region [ Helper methods ]

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
                dto.company_name,
                dto.website_url,
                dto.mail_address,
                dto.kind);
        }

        #endregion

        #region [ Query / Commands ]

        private const string GetBrandBySlugQuery = @"SELECT * FROM brands WHERE slug = @slug LIMIT 1;";
        private const string GetBrandByNameQuery = @"SELECT * FROM brands WHERE name = @name LIMIT 1;";
        private const string GetAllBrandsQuery = @"SELECT * FROM brands ORDER BY name;";
        private const string GetAllBrandsWithPaginationQuery = @"SELECT * FROM brands ORDER BY name LIMIT @limit OFFSET @skip;";

        private const string GetBrandExistsQuery = @"SELECT slug FROM brands WHERE slug = @slug LIMIT 1;";

        private const string InsertBrandCommand = @"INSERT INTO brands(
                brand_id, name, slug, company_name, mail_address, website_url, kind, created_at, version)
            VALUES(
                @BrandId, @Name, @Slug, @CompanyName, @EmailAddress, @WebsiteUrl, @Kind, @CreatedAt, @Version);";

        #endregion
    }
}
