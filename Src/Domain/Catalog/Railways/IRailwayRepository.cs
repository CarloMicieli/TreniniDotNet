using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwaysRepository
    {
        Task<RailwayId> Add(IRailway railway);

        Task<IRailway> GetBy(Slug slug);
    }
}
