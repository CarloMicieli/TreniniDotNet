using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScalesRepository
    {
        Task<ScaleId> Add(IScale scale);
        Task<IScale> GetBy(Slug slug);
        Task<Scale> GetByAsync(Slug slug);
        Task<ScaleId> Create(Scale scale);
    }
}
