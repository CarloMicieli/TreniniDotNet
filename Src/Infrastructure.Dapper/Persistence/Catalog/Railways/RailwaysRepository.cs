using System;
using System.Linq;
using System.Threading.Tasks;
using NodaTime.Extensions;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Railways
{
    public sealed class RailwaysRepository : IRailwaysRepository
    {
        private readonly DapperUnitOfWork _unitOfWork;

        public RailwaysRepository(DapperUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<PaginatedResult<Railway>> GetAllAsync(Page page)
        {
            var results = await _unitOfWork.QueryAsync<RailwayDto>(
                GetAllRailwaysWithPaginationQuery,
                new { @skip = page.Start, @limit = page.Limit + 1 });

            return new PaginatedResult<Railway>(
                page,
                results.Select(it => ProjectToDomain(it)!).ToList());
        }

        public async Task<RailwayId> AddAsync(Railway railway)
        {
            var _ = await _unitOfWork.ExecuteAsync(InsertRailwayCommand, new
            {
                railway.Id,
                railway.Name,
                railway.CompanyName,
                railway.Slug,
                Country = railway.Country.Code,
                railway.PeriodOfActivity?.OperatingSince,
                railway.PeriodOfActivity?.OperatingUntil,
                Active = railway.PeriodOfActivity?.RailwayStatus == RailwayStatus.Active,
                GaugeMm = railway.TrackGauge?.Millimeters,
                GaugeIn = railway.TrackGauge?.Inches,
                railway.TrackGauge?.TrackGauge,
                railway.Headquarters,
                TotalLengthMi = railway.TotalLength?.Miles,
                TotalLengthKm = railway.TotalLength?.Kilometers,
                railway.WebsiteUrl,
                Created = railway.CreatedDate.ToDateTimeUtc(),
                Modified = railway.ModifiedDate?.ToDateTimeUtc(),
                railway.Version
            });
            return railway.Id;
        }

        public async Task UpdateAsync(Railway railway)
        {
            var _ = await _unitOfWork.ExecuteAsync(UpdateRailwayCommand, new
            {
                railway.Id,
                railway.Name,
                railway.CompanyName,
                railway.Slug,
                railway.Country,
                railway.PeriodOfActivity?.OperatingSince,
                railway.PeriodOfActivity?.OperatingUntil,
                Active = railway.PeriodOfActivity?.RailwayStatus == RailwayStatus.Active,
                GaugeMm = railway.TrackGauge?.Millimeters,
                GaugeIn = railway.TrackGauge?.Inches,
                railway.TrackGauge?.TrackGauge,
                railway.Headquarters,
                TotalLengthMi = railway.TotalLength?.Miles,
                TotalLengthKm = railway.TotalLength?.Kilometers,
                railway.WebsiteUrl,
                Modified = railway.ModifiedDate?.ToDateTimeUtc(),
                railway.Version
            });
        }

        public Task DeleteAsync(RailwayId id)
        {
            return _unitOfWork.ExecuteScalarAsync<string>(
                DeleteRailwayCommand,
                new { Id = id });
        }
        public async Task<bool> ExistsAsync(Slug slug)
        {
            var result = await _unitOfWork.ExecuteScalarAsync<string>(
                GetRailwayExistsQuery,
                new { @slug = slug.ToString() });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<Railway?> GetBySlugAsync(Slug slug)
        {
            var result = await _unitOfWork.QueryFirstOrDefaultAsync<RailwayDto>(
                GetRailwayBySlugQuery,
                new { @slug = slug.ToString() });

            return ProjectToDomain(result);
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

            var railwayGauge = RailwayGauge.Create(dto.track_gauge, dto.gauge_in, dto.gauge_mm);
            var railwayLength = RailwayLength.Create(dto.total_length_km, dto.total_length_mi);

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
                dto.created.ToInstant(),
                dto.last_modified?.ToInstant(),
                dto.version ?? 1);
        }

        #region [ Commands / Query text ]

        private const string InsertRailwayCommand = @"INSERT INTO railways(
	            railway_id, name, company_name, slug, country, operating_since, operating_until, 
                active, gauge_mm, gauge_in, track_gauge, headquarters, total_length_mi, total_length_km, 
                website_url, created, last_modified, version)
            VALUES(@Id, @Name, @CompanyName, @Slug, @Country, @OperatingSince, @OperatingUntil, 
                @Active, @GaugeMm, @GaugeIn, @TrackGauge, @Headquarters, @TotalLengthMi, @TotalLengthKm, 
                @WebsiteUrl, @Created, @Modified, @Version);";

        private const string UpdateRailwayCommand = @"UPDATE railways SET 
                name = @Name, company_name = @CompanyName, slug = @Slug, country = @Country, 
                operating_since = @OperatingSince, operating_until = @OperatingUntil, active = @Active, 
                gauge_mm = @GaugeMm, gauge_in = @GaugeIn, track_gauge = @TrackGauge, 
                headquarters = @Headquarters, total_length_mi = @TotalLengthMi, total_length_km = @TotalLengthKm, 
                website_url = @WebsiteUrl, 
                last_modified = @Modified, 
                version = @Version
            WHERE railway_id = @Id;";

        private const string GetRailwayExistsQuery = @"SELECT slug FROM railways WHERE slug = @slug LIMIT 1;";

        private const string GetRailwayBySlugQuery = @"SELECT * FROM railways WHERE slug = @slug LIMIT 1;";

        private const string GetAllRailwaysWithPaginationQuery = @"SELECT * FROM railways ORDER BY name LIMIT @limit OFFSET @skip;";

        private const string DeleteRailwayCommand = @"DELETE FROM railways WHERE railway_id = @Id;";
        
        #endregion
    }
}
