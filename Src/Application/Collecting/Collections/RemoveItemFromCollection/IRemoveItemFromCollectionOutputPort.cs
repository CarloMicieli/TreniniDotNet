using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection
{
    public interface IRemoveItemFromCollectionOutputPort : IOutputPortStandard<RemoveItemFromCollectionOutput>
    {
        void CollectionItemNotFound(CollectionId collectionId, CollectionItemId itemId);
    }
}
