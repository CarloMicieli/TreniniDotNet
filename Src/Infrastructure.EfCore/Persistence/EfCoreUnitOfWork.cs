using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;

namespace TreniniDotNet.Infrastructure.Persistence
{
    public sealed class EfCoreUnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public EfCoreUnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<int> SaveAsync() =>
            _context.SaveChangesAsync();
    }
}
