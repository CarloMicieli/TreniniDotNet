using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Domain;

namespace TreniniDotNet.Infrastructure.Persistence
{
    public abstract class EfCoreRepository<TKey, TAggregateRoot> : IRepository<TKey, TAggregateRoot>
        where TKey : struct, IEquatable<TKey>
        where TAggregateRoot : AggregateRoot<TKey>
    {
        protected ApplicationDbContext DbContext { get; }

        protected EfCoreRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual Task<PaginatedResult<TAggregateRoot>> GetAllAsync(Page page)
        {
            return DbContext.Set<TAggregateRoot>()
                .AsQueryable()
                .ToPaginatedResultAsync(page, x => x.Id);
        }

        public virtual Task<TKey> AddAsync(TAggregateRoot aggregate)
        {
            DbContext.Set<TAggregateRoot>().Add(aggregate);
            return Task.FromResult(aggregate.Id);
        }

        public virtual Task UpdateAsync(TAggregateRoot aggregate)
        {
            var state = DbContext.Entry(aggregate);
            DbContext.Entry(aggregate).State = EntityState.Detached;
            DbContext.Set<TAggregateRoot>().Update(aggregate);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TAggregateRoot aggregate)
        {
            DbContext.Set<TAggregateRoot>().Remove(aggregate);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TKey id)
        {
            var aggregate = DbContext.Set<TAggregateRoot>().AsQueryable()
                .FirstOrDefault(it => it.Id.Equals(id));

            if (!(aggregate is null))
            {
                DbContext.Set<TAggregateRoot>().Remove(aggregate);
            }

            return Task.CompletedTask;
        }
    }
}
