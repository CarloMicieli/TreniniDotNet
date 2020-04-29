using System.Threading.Tasks;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public interface ICollectionsRepository
    {
        Task<ICollection?> GetByOwnerAsync(Owner owner);

        Task<bool> ExistsAsync(Owner owner);

        Task<bool> ExistsAsync(Owner owner, CollectionId id);

        Task<CollectionId?> GetIdByOwnerAsync(Owner owner);

        Task<CollectionId> AddAsync(ICollection collection);
    }
}
