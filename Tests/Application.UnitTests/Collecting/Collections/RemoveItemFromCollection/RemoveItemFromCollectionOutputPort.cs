using TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Collecting.Collections.OutputPorts
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
