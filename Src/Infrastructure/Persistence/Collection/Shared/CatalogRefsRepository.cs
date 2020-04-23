using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Shared
{
    public sealed class CatalogRefsRepository : ICatalogRefsRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public CatalogRefsRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext ??
                throw new ArgumentNullException(nameof(databaseContext));
        }

        public Task<ICatalogRef> GetBySlugAsync(Slug slug)
        {
            throw new System.NotImplementedException();
        }
    }
}
