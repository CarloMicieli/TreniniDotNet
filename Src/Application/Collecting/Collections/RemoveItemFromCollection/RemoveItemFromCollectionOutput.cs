using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection
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
