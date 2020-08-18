using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class CollectionItemsFactory : EntityFactory<CollectionItemId, CollectionItem>
    {
        public CollectionItemsFactory(IGuidSource guidSource)
            : base(guidSource)
        {
        }

        public CollectionItem CreateCollectionItem(
            CatalogItemRef catalogItem,
            Condition condition,
            Price price,
            ShopRef? purchasedAt,
            LocalDate addedDate,
            string? notes)
        {
            return new CollectionItem(
                NewId(id => new CollectionItemId(id)),
                catalogItem,
                condition,
                price,
                purchasedAt,
                addedDate,
                null,
                notes);
        }
    }
}
