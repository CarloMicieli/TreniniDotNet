using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

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
