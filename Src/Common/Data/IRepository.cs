using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Domain;

namespace TreniniDotNet.Common.Data
{
    public interface IRepository<TKey, TAggregateRoot>
        where TKey : struct, IEquatable<TKey>
        where TAggregateRoot : AggregateRoot<TKey>
    {
        Task<PaginatedResult<TAggregateRoot>> GetAllAsync(Page page);

        Task<TKey> AddAsync(TAggregateRoot aggregate);

        Task UpdateAsync(TAggregateRoot brand);

        Task DeleteAsync(TKey id);
    }
}
