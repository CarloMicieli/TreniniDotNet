using System;
using System.Threading.Tasks;
using Dapper;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Common
{
    public sealed class CatalogRefsRepository : ICatalogRefsRepository
    {
        private readonly IDatabaseContext _dbContext;

        public CatalogRefsRepository(IDatabaseContext databaseContext)
        {
            _dbContext = databaseContext ??
                throw new ArgumentNullException(nameof(databaseContext));
        }

        public async Task<ICatalogRef?> GetBySlugAsync(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<Guid?>(
                GetCatalogItemRef, new { Slug = slug.Value });

            if (result.HasValue)
            {
                return CatalogRef.Of(result.Value, slug.Value);
            }

            return null;
        }


        #region [ Query / Command ]

        private const string GetCatalogItemRef = @"SELECT catalog_item_id FROM catalog_items WHERE slug = @Slug;";

        #endregion
    }
}
