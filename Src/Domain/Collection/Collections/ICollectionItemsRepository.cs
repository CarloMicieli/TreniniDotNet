using NodaTime;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public interface ICollectionItemsRepository
    {
        Task<bool> ItemExistsAsync(CollectionId id, CollectionItemId itemId);

        Task<ICollectionItem?> GetItemByIdAsync(CollectionId collectionId, CollectionItemId itemId);

        Task<CollectionItemId> AddItemAsync(CollectionId id, ICollectionItem newItem);

        Task EditItemAsync(CollectionId id, ICollectionItem item);

        Task RemoveItemAsync(CollectionId collectionId, CollectionItemId itemId, LocalDate? removed, string? notes);
    }
}
