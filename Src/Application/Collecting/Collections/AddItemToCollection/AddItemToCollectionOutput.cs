using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.SharedKernel.Slugs;

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
