using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TreniniDotNet.Infrastructure.Persistence
{
    public static class EfEntityEntryExtensions
    {
        public static void MarkAsUnchanged<TEntity>(this EntityEntry<TEntity> entity)
            where TEntity : class
        {
            if (entity.State != EntityState.Detached)
            {
                entity.State = EntityState.Unchanged;
            }
        }
    }
}