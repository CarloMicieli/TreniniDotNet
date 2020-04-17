using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection
{
    public sealed class RemoveItemFromCollectionOutput : IUseCaseOutput
    {
        public RemoveItemFromCollectionOutput(CollectionId collectionId, CollectionItemId itemId)
        {
            CollectionId = collectionId;
            ItemId = itemId;
        }

        public CollectionId CollectionId { get; }
        public CollectionItemId ItemId { get; }
    }
}
