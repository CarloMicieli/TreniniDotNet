using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection
{
    public sealed class RemoveItemFromCollectionOutputPort : OutputPortTestHelper<RemoveItemFromCollectionOutput>, IRemoveItemFromCollectionOutputPort
    {
        private MethodInvocation<CollectionId> CollectionNotFoundMethod { set; get; }
        private MethodInvocation<CollectionId, CollectionItemId> CollectionItemNotFoundMethod { set; get; }

        public RemoveItemFromCollectionOutputPort()
        {
            CollectionNotFoundMethod = MethodInvocation<CollectionId>.NotInvoked(nameof(CollectionNotFound));
            CollectionItemNotFoundMethod = MethodInvocation<CollectionId, CollectionItemId>.NotInvoked(nameof(CollectionItemNotFound));
        }

        public void CollectionNotFound(CollectionId collectionId)
        {
            CollectionNotFoundMethod = CollectionNotFoundMethod.Invoked(collectionId);
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

        public void AssertCollectionWasNotFoundForId(CollectionId expectedCollectionId)
        {
            this.CollectionNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedCollectionId);
        }
    }
}
