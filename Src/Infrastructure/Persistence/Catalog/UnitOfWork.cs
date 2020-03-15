using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;

namespace TreniniDotNet.Infrastructure.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAsync()
        {
            int affectedRows = await _context.SaveChangesAsync().ConfigureAwait(false);
            return affectedRows;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}