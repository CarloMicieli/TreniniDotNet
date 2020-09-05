using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection
{
    public interface IRemoveItemFromCollectionOutputPort : IStandardOutputPort<RemoveItemFromCollectionOutput>
    {
        void CollectionNotFound(CollectionId collectionId);

        void CollectionItemNotFound(CollectionId collectionId, CollectionItemId itemId);
    }
}
