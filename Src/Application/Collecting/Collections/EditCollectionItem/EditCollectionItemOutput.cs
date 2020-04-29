using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Collections.EditCollectionItem
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
