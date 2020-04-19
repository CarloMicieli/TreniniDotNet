using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public interface ICollectionsRepository
    {
        Task<ICollection?> GetByOwnerAsync(Owner owner);

        Task<bool> ExistsAsync(Owner owner);

        Task<bool> ExistsAsync(CollectionId id);

        Task<CollectionId?> GetIdByOwnerAsync(Owner owner);

        Task<CollectionId> AddAsync(ICollection collection);
    }
}
