using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection
{
    public interface IRemoveItemFromCollectionOutputPort : IOutputPortStandard<RemoveItemFromCollectionOutput>
    {
        void CollectionItemNotFound(CollectionId collectionId, CollectionItemId itemId);
    }
}
