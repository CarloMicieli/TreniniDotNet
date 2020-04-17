using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public interface ICollectionsRepository
    {
        Task<ICollection> GetByOwnerAsync(Owner owner);

        Task<bool> AnyByOwnerAsync(string owner);

        Task<bool> ExistsAsync(CollectionId id);

        Task<CollectionItemId> AddItemAsync(CollectionId id, ICollectionItem newItem);

        Task EditItemAsync(CollectionId id, ICollectionItem item);

        Task DeleteItemAsync(CollectionId id, CollectionItemId itemId);

        Task<ICollectionStats> GetStatisticsAsync(string owner);

        Task<ICollectionItem> GetCollectionItemByIdAsync(CollectionId collectionId, CollectionItemId itemId);

        Task<CollectionId?> GetIdByOwnerAsync(Owner owner);
    }
}
