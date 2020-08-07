using System;
using System.Linq;
using System.Threading.Tasks;
using NodaTime.Extensions;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Railways
{
    public sealed class RailwaysRepository : IRailwaysRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public RailwaysRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<PaginatedResult<Railway>> GetRailwaysAsync(Page page)
        {
            var results = await _unitOfWork.QueryAsync<RailwayDto>(
                GetAllRailwaysWithPaginationQuery,
                new { skip = page.Start, limit = page.Limit + 1 });

            return new PaginatedResult<Railway>(
                page,
                results.Select(it => ProjectToDomain(it)!).ToList());
        }

        public async Task<RailwayId> AddAsync(Railway railway)
        {
            var _ = await _unitOfWork.ExecuteAsync(InsertRailwayCommand, 
                ToRailwayDto(railway));
            return railway.Id;
        }

        public async Task UpdateAsync(Railway railway)
        {
            var _ = await _unitOfWork.ExecuteAsync(UpdateRailwayCommand, 
                ToRailwayDto(railway));
        }

        public Task DeleteAsync(RailwayId id)
        {
            return _unitOfWork.ExecuteScalarAsync<string>(
                DeleteRailwayCommand,
                new { railway_id = id.ToGuid() });
        }
        public async Task<bool> ExistsAsync(Slug slug)
        {
            var result = await _unitOfWork.ExecuteScalarAsync<string>(
                GetRailwayExistsQuery,
                new { slug = slug.Value });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<Railway?> GetBySlugAsync(Slug slug)
        {
            var result = await _unitOfWork.QueryFirstOrDefaultAsync<RailwayDto>(
                GetRailwayBySlugQuery,
                new { @slug = slug.ToString() });

            return ProjectToDomain(result);
        }

        private RailwayDto ToRailwayDto(Railway railway)
        {
            return new RailwayDto
            {
                railway_id = railway.Id.ToGuid(),
                name = railway.Name,
                company_name = railway.CompanyName,
                slug = railway.Slug,
                country = railway.Country.Code,
                operating_since = railway.PeriodOfActivity?.OperatingSince,
                operating_until = railway.PeriodOfActivity?.OperatingUntil,
                active = railway.PeriodOfActivity?.RailwayStatus == RailwayStatus.Active,
                gauge_mm = railway.TrackGauge?.Millimeters.Value,
                gauge_in = railway.TrackGauge?.Inches.Value,
                track_gauge = railway.TrackGauge?.TrackGauge.ToString(),
                headquarters = railway.Headquarters,
                total_length_mi = railway.TotalLength?.Miles.Value,
                total_length_km = railway.TotalLength?.Kilometers.Value,
                website_url = railway.WebsiteUrl?.ToString(),
                created = railway.CreatedDate.ToDateTimeUtc(),
                last_modified = railway.ModifiedDate?.ToDateTimeUtc(),
                version = railway.Version
            };
        }
        
        private Railway? ProjectToDomain(RailwayDto? dto)
        {
            if (dto is null)
            {
                return null;
            }

            var periodOfActivity = PeriodOfActivity.Of(
                dto.active == true ? RailwayStatus.Active.ToString() : RailwayStatus.Inactive.ToString(),
                dto.operating_since,
                dto.operating_until);

            RailwayGauge.TryCreate(dto.track_gauge, dto.gauge_in, dto.gauge_mm, out var railwayGauge);
            RailwayLength.TryCreate(dto.total_length_km, dto.total_length_mi, out var railwayLength);

            return new Railway(
                new RailwayId(dto.railway_id),
                dto.name,
                dto.company_name,
                Country.Of(dto.country!),
                periodOfActivity,
                railwayLength,
                railwayGauge,
                dto.website_url.ToUriOpt(),
                dto.headquarters,
                dto.created.ToUtc(),
                dto.last_modified.ToUtcOrDefault(),
                dto.version ?? 1);
        }

        #region [ Commands / Query text ]

        private const string InsertRailwayCommand = @"INSERT INTO railways(
	            railway_id, name, company_name, slug, country, operating_since, operating_until, 
                active, gauge_mm, gauge_in, track_gauge, headquarters, total_length_mi, total_length_km, 
                website_url, created, last_modified, version)
            VALUES(@railway_id, @name, @company_name, @slug, @country, @operating_since, @operating_until, 
                @active, @gauge_mm, @gauge_in, @track_gauge, @headquarters, @total_length_mi, @total_length_km, 
                @website_url, @created, @last_modified, @version);";

        private const string UpdateRailwayCommand = @"UPDATE railways SET 
                name = @name, company_name = @company_name, slug = @slug, country = @country, 
                operating_since = @operating_since, operating_until = @operating_until, active = @active, 
                gauge_mm = @gauge_mm, gauge_in = @gauge_in, track_gauge = @track_gauge, 
                headquarters = @headquarters, total_length_mi = @total_length_mi, total_length_km = @total_length_km, 
                website_url = @website_url, created = @created,last_modified = @last_modified, 
                version = @version
            WHERE railway_id = @railway_id;";

        private const string GetRailwayExistsQuery = @"SELECT slug FROM railways WHERE slug = @slug LIMIT 1;";

        private const string GetRailwayBySlugQuery = @"SELECT * FROM railways WHERE slug = @slug LIMIT 1;";

        private const string GetAllRailwaysWithPaginationQuery = @"SELECT * FROM railways ORDER BY name LIMIT @limit OFFSET @skip;";

        private const string DeleteRailwayCommand = @"DELETE FROM railways WHERE railway_id = @railway_id;";

        #endregion
    }
}
