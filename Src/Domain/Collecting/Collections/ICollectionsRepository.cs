using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public interface ICollectionsRepository : IRepository<CollectionId, Collection>
    {
        Task<bool> ExistsAsync(CollectionId id);

        Task<Collection?> GetByIdAsync(CollectionId id);

        Task<Collection?> GetByOwnerAsync(Owner owner);

        Task<bool> ExistsAsync(Owner owner);
    }
}
