using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public sealed class RemoveItemFromCollectionOutputPort : OutputPortTestHelper<RemoveItemFromCollectionOutput>, IRemoveItemFromCollectionOutputPort
    {
        private MethodInvocation<CollectionId, CollectionItemId> CollectionItemNotFoundMethod { set; get; }

        public RemoveItemFromCollectionOutputPort()
        {
            CollectionItemNotFoundMethod = MethodInvocation<CollectionId, CollectionItemId>.NotInvoked(nameof(CollectionItemNotFound));
        }

        public void CollectionItemNotFound(CollectionId collectionId, CollectionItemId itemId)
        {
            CollectionItemNotFoundMethod = CollectionItemNotFoundMethod.Invoked(collectionId, itemId);
        }

        public void AssertCollectionItemWasNotFoundForId(
            CollectionId expectedCollectionId,
            CollectionItemId expectedCollectionItemId)
        {
            this.CollectionItemNotFoundMethod.ShouldBeInvokedWithTheArguments(
                expectedCollectionId,
                expectedCollectionItemId);
        }
    }
}
