using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class CollectionItemsFactory : EntityFactory<CollectionItemId, CollectionItem>
    {
        public CollectionItemsFactory(IGuidSource guidSource)
            : base(guidSource)
        {
        }

        public CollectionItem CreateCollectionItem(
            CatalogItem catalogItem,
            Condition condition,
            Price price,
            Shop? purchasedAt,
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
