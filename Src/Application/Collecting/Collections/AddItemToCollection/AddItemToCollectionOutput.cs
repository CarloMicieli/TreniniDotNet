using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
{
    public sealed class AddItemToCollectionOutput : IUseCaseOutput
    {
        public AddItemToCollectionOutput(CollectionId collectionId, CollectionItemId itemId, Slug catalogItem)
        {
            CollectionId = collectionId;
            ItemId = itemId;
            CatalogItem = catalogItem;
        }

        public CollectionId CollectionId { get; }
        public CollectionItemId ItemId { get; }
        public Slug CatalogItem { get; }
    }
}
