using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using CatalogTables = TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Tables;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Collections
{
    public static class DatabaseTestHelpersExtensions
    {
        public static void ArrangeWithCollection(this DatabaseTestHelpers database, Collection collection)
        {
            database.Setup.TruncateTable(Tables.CollectionItems);
            database.Setup.TruncateTable(Tables.Collections);
            
            database.Setup.TruncateTable(Tables.Shops);
            
            database.Setup.TruncateTable(CatalogTables.RollingStocks);
            database.Setup.TruncateTable(CatalogTables.CatalogItems);
            database.Setup.TruncateTable(CatalogTables.Brands);
            database.Setup.TruncateTable(CatalogTables.Railways);
            database.Setup.TruncateTable(CatalogTables.Scales);
            
            database.Arrange.ArrangeCatalogData();

            var shop = CollectingSeedData.Shops.NewModellbahnshopLippe();
            
            database.Arrange.InsertOne(Tables.Shops, new
            {
                shop_id = shop.Id.ToGuid(),
                name = shop.Name,
                slug = shop.Slug.Value,
                created = shop.CreatedDate.ToDateTimeUtc(),
                last_modified = shop.ModifiedDate?.ToDateTimeUtc(),
                version = shop.Version
            });
            
            database.Arrange.InsertOne(Tables.Collections, new
            {
                collection_id = collection.Id.ToGuid(),
                owner = collection.Owner.Value,
                notes = collection.Notes,
                created = collection.CreatedDate.ToDateTimeUtc(),
                last_modified = collection.ModifiedDate?.ToDateTimeUtc(),
                version = collection.Version
            });

            foreach (var item in collection.Items)
            {
                database.Arrange.InsertOne(Tables.CollectionItems, new
                {
                    collection_id = collection.Id.ToGuid(),
                    collection_item_id = item.Id.ToGuid(),
                    catalog_item_id = item.CatalogItem.Id.ToGuid(),
                    notes = item.Notes,
                    price = item.Price.Amount,
                    currency = item.Price.Currency,
                    condition = item.Condition.ToString(),
                    purchased_at = item.PurchasedAt?.Id.ToGuid(),
                    added_date = item.AddedDate.ToDateTimeUnspecified()
                });
            }
        }
    }
}