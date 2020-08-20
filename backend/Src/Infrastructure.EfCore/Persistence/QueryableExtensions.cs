using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Common.Data.Pagination;

namespace TreniniDotNet.Infrastructure.Persistence
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<TValue>> ToPaginatedResultAsync<TValue, TKey>(
            this IQueryable<TValue> queryable, 
            Page page,
            Expression<Func<TValue, TKey>> orderBy)
        {
            var results = await queryable
                .OrderBy(orderBy)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .ToListAsync();

            return new PaginatedResult<TValue>(page, results);
        }
    }
}