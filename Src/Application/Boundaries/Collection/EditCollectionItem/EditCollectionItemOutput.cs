using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem
{
    public sealed class EditCollectionItemOutput : IUseCaseOutput
    {
        public EditCollectionItemOutput(CollectionId collectionId, CollectionItemId itemId, Slug catalogItem)
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
