using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public interface ICollectionsRepository
    {
        Task<ICollection> GetByOwnerAsync(string owner);

        Task AddItemAsync(CollectionId id, ICollectionItem newItem);

        Task EditItemAsync(CollectionId id, ICollectionItem item);

        Task DeleteItemAsync(CollectionId id, CollectionItemId itemId);

        Task<ICollectionStats> GetStatisticsAsync(string owner);
    }
}
