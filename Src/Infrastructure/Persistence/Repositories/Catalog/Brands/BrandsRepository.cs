using System;
using System.Linq;
using System.Threading.Tasks;
using NodaTime.Extensions;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Brands
{
    public sealed class BrandsRepository : IBrandsRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<PaginatedResult<Brand>> GetBrandsAsync(Page page)
        {
            var results = await _unitOfWork.QueryAsync<BrandDto>(
                GetAllBrandsWithPaginationQuery,
                new { @skip = page.Start, @limit = page.Limit + 1 });

            return new PaginatedResult<Brand>(
                page,
                results.Select(b => FromBrandDto(b)!).ToList());
        }

        public async Task<BrandId> AddAsync(Brand brand)
        {
            var _ = await _unitOfWork.ExecuteAsync(InsertBrandCommand, new
            {
                Id = brand.Id.ToGuid(),
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
            return brand.Id;
        }

        public async Task UpdateAsync(Brand brand)
        {
            var _ = await _unitOfWork.ExecuteAsync(UpdateBrandCommand, new
            {
                Id = brand.Id.ToGuid(),
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

        public Task DeleteAsync(BrandId id)
        {
            return _unitOfWork.ExecuteScalarAsync<string>(
                DeleteBrandCommand,
                new { Id = id.ToGuid() });
        }

        public async Task<bool> ExistsAsync(Slug slug)
        {
            var result = await _unitOfWork.ExecuteScalarAsync<string>(
                GetBrandExistsQuery,
                new { slug = slug.ToString() });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<Brand?> GetBySlugAsync(Slug slug)
        {
            var result = await _unitOfWork.QueryFirstOrDefaultAsync<BrandDto>(
                GetBrandBySlugQuery,
                new { @slug = slug.ToString() });

            return FromBrandDto(result);
        }

        #region [ Helper methods ]

        private Brand? FromBrandDto(BrandDto? dto)
        {
            if (dto is null)
            {
                return null;
            }

            Address? address = null;

            if (!string.IsNullOrWhiteSpace(dto.address_line1) &&
                !string.IsNullOrWhiteSpace(dto.address_city) &&
                !string.IsNullOrWhiteSpace(dto.address_postal_code))
            {
                address = Address.With(
                    dto.address_line1,
                    dto.address_line2,
                    dto.address_city,
                    dto.address_region,
                    dto.address_postal_code,
                    dto.address_city);
            }

            return new Brand(
                new BrandId(dto.brand_id),
                Slug.Of(dto.slug),
                dto.name,
                dto.website_url.ToUriOpt(),
                dto.mail_address.ToMailAddressOpt(),
                dto.company_name,
                dto.group_name,
                dto.description,
                address,
                EnumHelpers.RequiredValueFor<BrandKind>(dto.kind),
                dto.created.ToUtc(),
                dto.last_modified.ToUtcOrDefault(),
                dto.version ?? 1);
        }

        #endregion

        #region [ Query / Commands ]

        private const string GetBrandBySlugQuery = @"SELECT * FROM brands WHERE slug = @slug LIMIT 1;";

        private const string GetAllBrandsWithPaginationQuery = @"SELECT * FROM brands ORDER BY name LIMIT @limit OFFSET @skip;";

        private const string GetBrandExistsQuery = @"SELECT slug FROM brands WHERE slug = @slug LIMIT 1;";

        private const string InsertBrandCommand = @"INSERT INTO brands(
                brand_id, name, slug, company_name, group_name, description, 
                address_line1, address_line2, address_city, address_region, address_postal_code, address_country,
                mail_address, website_url, kind, created, last_modified, version)
            VALUES(
                @Id, @Name, @Slug, @CompanyName, @GroupName, @Description, 
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
            WHERE brand_id = @Id;";

        private const string DeleteBrandCommand = @"DELETE FROM brands WHERE brand_id = @Id";

        #endregion
    }
}
