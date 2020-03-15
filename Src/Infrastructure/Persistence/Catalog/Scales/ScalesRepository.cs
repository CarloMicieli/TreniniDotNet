using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using System.Collections.Generic;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Infrastracture.Dapper;
using Dapper;
using System.Linq;

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

        public async Task<ScaleId> Add(IScale scale)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(InsertScaleCommand, scale);
            return scale.ScaleId;
        }

        public async Task<bool> Exists(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteScalarAsync<string>(
                GetScaleExistsQuery,
                new { slug = slug.ToString() });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<List<IScale>> GetAll()
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryAsync<ScaleDto>(
                GetAllScalesQuery,
                new { });

            if (result is null)
            {
                return new List<IScale>();
            }

            return result.Select(it => FromScaleDto(it)!).ToList();
        }

        public async Task<IScale?> GetByName(string name)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<ScaleDto>(
                GetScaleByNameQuery,
                new { name });

            return FromScaleDto(result);
        }

        public async Task<IScale?> GetBySlug(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<ScaleDto>(
                GetScaleBySlugQuery,
                new { @slug = slug.ToString() });

            return FromScaleDto(result);
        }

        public async Task<PaginatedResult<IScale>> GetScales(Page page)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var results = await connection.QueryAsync<ScaleDto>(
                GetAllScalesWithPaginationQuery,
                new { skip = page.Start, limit = page.Limit + 1 });

            return new PaginatedResult<IScale>(
                page,
                results.Select(it => FromScaleDto(it)!).ToList());
        }

        private IScale? FromScaleDto(ScaleDto? dto)
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
                dto.gauge,
                dto.track_type,
                dto.notes);
        }

        #region [ Query / Command text ]

        private const string InsertScaleCommand = @"INSERT INTO scales(
	            scale_id, name, slug, ratio, gauge, track_type, notes, created_at, version)
            VALUES(
                @ScaleId, @Name, @Slug, @Ratio, @Gauge, @TrackGauge, @Notes, @CreatedAt, @Version);";

        private const string GetScaleExistsQuery = @"SELECT slug FROM scales WHERE slug = @slug LIMIT 1;";
        private const string GetAllScalesQuery = @"SELECT * FROM scales ORDER BY name;";
        private const string GetScaleBySlugQuery = @"SELECT * FROM scales WHERE slug = @slug LIMIT 1;";
        private const string GetScaleByNameQuery = @"SELECT * FROM scales WHERE name = @name LIMIT 1;";
        private const string GetAllScalesWithPaginationQuery = @"SELECT * FROM scales ORDER BY name OFFSET @skip LIMIT @limit;";

        #endregion
    }
}