using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Infrastracture.Dapper;

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

        public async Task<RailwayId> Add(IRailway railway)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(InsertRailwayCommand, railway);
            return railway.RailwayId;
        }

        public async Task<bool> Exists(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteScalarAsync<string>(
                GetRailwayExistsQuery,
                new { slug = slug.ToString() });

            return string.IsNullOrEmpty(result);
        }

        public async Task<List<IRailway>> GetAll()
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryAsync<RailwayDto>(
                GetAllRailwaysQuery,
                new { });

            if (result is null)
            {
                return new List<IRailway>();
            }

            return result.Select(it => FromRailwayDto(it)!).ToList();
        }

        public async Task<IRailway?> GetBySlug(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<RailwayDto>(
                GetRailwayBySlugQuery,
                new { @slug = slug.ToString() });

            return FromRailwayDto(result);
        }

        public async Task<IRailway?> GetByName(string name)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<RailwayDto>(
                GetRailwayByNameQuery,
                new { name });

            return FromRailwayDto(result);
        }

        public async Task<PaginatedResult<IRailway>> GetRailways(Page page)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var results = await connection.QueryAsync<RailwayDto>(
                GetAllRailwaysWithPaginationQuery,
                new { skip = page.Start, limit = page.Limit + 1 });

            return new PaginatedResult<IRailway>(
                page,
                results.Select(it => FromRailwayDto(it)!).ToList());
        }

        private IRailway? FromRailwayDto(RailwayDto? dto)
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
                dto.created_at,
                dto.version);
        }

        #region [ Commands / Query text ]

        private const string InsertRailwayCommand = @"INSERT INTO railways(
	            railway_id, name, company_name, slug, country, operating_since, operating_until, 
                active, created_at, version)
            VALUES(@RailwayId, @Name, @CompanyName, @Slug, @Country, @OperatingSince, @OperatingUntil, 
                @Active, @CreatedAt, @Version);";

        private const string GetRailwayExistsQuery = @"SELECT TOP 1 slug FROM railways WHERE slug = @slug;";
        private const string GetAllRailwaysQuery = @"SELECT * FROM railways ORDER BY name;";
        private const string GetRailwayBySlugQuery = @"SELECT TOP 1 * FROM railways WHERE slug = @slug;";
        private const string GetRailwayByNameQuery = @"SELECT TOP 1 * FROM railways WHERE name = @name;";
        private const string GetAllRailwaysWithPaginationQuery = @"SELECT * FROM railways ORDER BY name OFFSET @skip LIMIT @limit;";

        #endregion
    }
}
