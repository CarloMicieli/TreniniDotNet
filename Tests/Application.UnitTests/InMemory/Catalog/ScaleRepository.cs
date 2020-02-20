using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.InMemory.Catalog
{
    public sealed class ScaleRepository : IScalesRepository
    {
        public Task<ScaleId> Add(IScale scale)
        {
            throw new System.NotImplementedException();
        }

        public Task<ScaleId> Create(Scale scale)
        {
            throw new System.NotImplementedException();
        }

        public Task<IScale> GetBy(Slug slug)
        {
            throw new System.NotImplementedException();
        }

        public Task<Scale> GetByAsync(Slug slug)
        {
            throw new System.NotImplementedException();
        }
    }
}
