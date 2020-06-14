using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Pagination;

namespace TreniniDotNet.Common
{
    public interface IRepository<TKey, TAggregateRoot>
        where TKey : IEquatable<TKey>
        where TAggregateRoot : AggregateRoot<TKey>
    {

        Task<bool> ExistsAsync(TKey id);

        Task<TAggregateRoot?> GetByIdAsync(TKey id);

        Task<PaginatedResult<TAggregateRoot>> GetAllAsync(Page page);

        Task<TKey> AddAsync(TAggregateRoot aggregateRoot);

        Task UpdateAsync(TAggregateRoot aggregateRoot);

        Task DeleteAsync(TKey id);

    }
}
