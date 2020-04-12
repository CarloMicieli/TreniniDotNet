using Dapper;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Railways
{
    public sealed class RailwaysRepository : IRailwaysRepository
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IRailwaysFactory _railwaysFactory;

        public RailwaysRepository(IDatabaseContext context, IRailwaysFactory railwaysFactory)
        {
            _dbContext = context;
            _railwaysFactory = railwaysFactory;
        }

        public async Task<RailwayId> AddAsync(IRailway railway)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(InsertRailwayCommand, new
            {
                railway.RailwayId,
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
                Created = railway.CreatedDate.ToDateTimeUtc(),
                Modified = railway.ModifiedDate?.ToDateTimeUtc(),
                railway.Version
            });
            return railway.RailwayId;
        }

        public async Task<bool> ExistsAsync(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteScalarAsync<string>(
                GetRailwayExistsQuery,
                new { @slug = slug.ToString() });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<IRailway?> GetBySlugAsync(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<RailwayDto>(
                GetRailwayBySlugQuery,
                new { @slug = slug.ToString() });

            return ProjectToDomain(result);
        }

        public async Task<PaginatedResult<IRailway>> GetRailwaysAsync(Page page)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var results = await connection.QueryAsync<RailwayDto>(
                GetAllRailwaysWithPaginationQuery,
                new { @skip = page.Start, @limit = page.Limit + 1 });

            return new PaginatedResult<IRailway>(
                page,
                results.Select(it => ProjectToDomain(it)!).ToList());
        }

        private IRailway? ProjectToDomain(RailwayDto? dto)
        {
            if (dto is null)
            {
                return null;
            }

            return _railwaysFactory.NewRailway(
                dto.railway_id,
                dto.name,
                dto.slug,
                dto.company_name,
                dto.country,
                dto.operating_since,
                dto.operating_until,
                dto.active,
                dto.gauge_mm,
                dto.gauge_in,
                dto.track_gauge,
                dto.headquarters,
                dto.total_length_mi,
                dto.total_length_km,
                dto.website_url,
                dto.created,
                dto.last_modified,
                dto.version);
        }

        #region [ Commands / Query text ]

        private const string InsertRailwayCommand = @"INSERT INTO railways(
	            railway_id, name, company_name, slug, country, operating_since, operating_until, 
                active, gauge_mm, gauge_in, track_gauge, headquarters, total_length_mi, total_length_km, 
                website_url, created, last_modified, version)
            VALUES(@RailwayId, @Name, @CompanyName, @Slug, @Country, @OperatingSince, @OperatingUntil, 
                @Active, @GaugeMm, @GaugeIn, @TrackGauge, @Headquarters, @TotalLengthMi, @TotalLengthKm, 
                @WebsiteUrl, @Created, @Modified, @Version);";

        private const string GetRailwayExistsQuery = @"SELECT slug FROM railways WHERE slug = @slug LIMIT 1;";
        private const string GetRailwayBySlugQuery = @"SELECT * FROM railways WHERE slug = @slug LIMIT 1;";
        private const string GetAllRailwaysWithPaginationQuery = @"SELECT * FROM railways ORDER BY name LIMIT @limit OFFSET @skip;";

        #endregion
    }
}
