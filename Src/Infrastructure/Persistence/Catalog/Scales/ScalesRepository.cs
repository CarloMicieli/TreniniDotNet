using System.Threading.Tasks;
using System;
using Dapper;
using System.Linq;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Scales
{
    public sealed class ScalesRepository : IScalesRepository
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IScalesFactory _scalesFactory;

        public ScalesRepository(IDatabaseContext context, IScalesFactory scalesFactory)
        {
            _dbContext = context ??
                throw new ArgumentNullException(nameof(context));

            _scalesFactory = scalesFactory ??
                throw new ArgumentNullException(nameof(scalesFactory));
        }

        public async Task<ScaleId> AddAsync(IScale scale)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(InsertScaleCommand, new
            {
                scale.ScaleId,
                scale.Name,
                scale.Slug,
                scale.Ratio,
                GaugeMm = scale.Gauge.InMillimeters.Value,
                GaugeIn = scale.Gauge.InInches.Value,
                TrackGauge = scale.Gauge.TrackGauge.ToString(),
                scale.Description,
                scale.Weight,
                Created = scale.CreatedDate.ToDateTimeUtc(),
                Modified = scale.ModifiedDate?.ToDateTimeUtc(),
                scale.Version
            });
            return scale.ScaleId;
        }

        public async Task UpdateAsync(IScale scale)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var _ = await connection.ExecuteAsync(UpdateScaleCommand, new
            {
                scale.ScaleId,
                scale.Name,
                scale.Slug,
                scale.Ratio,
                GaugeMm = scale.Gauge.InMillimeters.Value,
                GaugeIn = scale.Gauge.InInches.Value,
                TrackGauge = scale.Gauge.TrackGauge.ToString(),
                scale.Description,
                scale.Weight,
                Modified = scale.ModifiedDate?.ToDateTimeUtc(),
                scale.Version
            });
        }

        public async Task<bool> ExistsAsync(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteScalarAsync<string>(
                GetScaleExistsQuery,
                new { slug = slug.ToString() });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<IScaleInfo?> GetInfoBySlugAsync(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<ScaleInfoDto?>(
                GetScaleInfoBySlugQuery,
                new { @slug = slug.ToString() });

            if (result is null)
            {
                return null;
            }

            return ProjectInfoToDomain(result);
        }

        public async Task<IScale?> GetBySlugAsync(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<ScaleDto>(
                GetScaleBySlugQuery,
                new { @slug = slug.ToString() });

            return ProjectToDomain(result);
        }

        public async Task<PaginatedResult<IScale>> GetScalesAsync(Page page)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var results = await connection.QueryAsync<ScaleDto>(
                GetAllScalesWithPaginationQuery,
                new { @skip = page.Start, @limit = page.Limit + 1 });

            return new PaginatedResult<IScale>(
                page,
                results.Select(it => ProjectToDomain(it)!).ToList());
        }

        private IScaleInfo ProjectInfoToDomain(ScaleInfoDto dto)
        {
            return new ScaleInfo(dto.scale_id, dto.slug, dto.name, dto.ratio);
        }

        private IScale? ProjectToDomain(ScaleDto? dto)
        {
            if (dto is null)
            {
                return null;
            }

            return _scalesFactory.NewScale(
                dto.scale_id,
                dto.name,
                dto.slug,
                dto.ratio,
                dto.gauge_mm,
                dto.gauge_in,
                dto.track_type,
                dto.description,
                dto.weight,
                dto.created,
                dto.last_modified,
                dto.version);
        }

        #region [ Query / Command text ]

        private const string InsertScaleCommand = @"INSERT INTO scales(
	            scale_id, name, slug, ratio, gauge_mm, gauge_in, track_type, description, weight, created, last_modified, version)
            VALUES(
                @ScaleId, @Name, @Slug, @Ratio, @GaugeMm, @GaugeIn, @TrackGauge, @Description, @Weight, @Created, @Modified, @Version);";

        private const string UpdateScaleCommand = @"UPDATE scales SET 
                name = @Name, 
                slug = @Slug, 
                ratio = @Ratio, 
                gauge_mm = @GaugeMm, 
                gauge_in = @GaugeIn, 
                track_type = @TrackGauge, 
                description = @Description, 
                weight = @Weight, 
                last_modified = @Modified, 
                version = @Version
            WHERE scale_id = @ScaleId;";

        private const string GetScaleExistsQuery = @"SELECT slug FROM scales WHERE slug = @slug LIMIT 1;";

        private const string GetScaleBySlugQuery = @"SELECT * FROM scales WHERE slug = @slug LIMIT 1;";

        private const string GetScaleInfoBySlugQuery = @"SELECT scale_id, name, slug, ratio FROM scales WHERE slug = @slug LIMIT 1;";

        private const string GetAllScalesWithPaginationQuery = @"SELECT * FROM scales ORDER BY name LIMIT @limit OFFSET @skip;";

        #endregion
    }
}