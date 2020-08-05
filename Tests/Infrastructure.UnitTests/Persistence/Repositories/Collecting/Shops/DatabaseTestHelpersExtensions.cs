using System;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Shops
{
    public static class DatabaseTestHelpersExtensions
    {
        public static void ArrangeWithShopFavourites(this DatabaseTestHelpers database, Owner owner, params Shop[] shops)
        {
            foreach (var shop in shops)
            {
                database.ArrangeWithOneShop(shop);
                
                database.Arrange.InsertOne(Tables.ShopFavourites, new
                {
                    owner = owner.Value,
                    shop_id = shop.Id.ToGuid()
                });
            }
        }
        
        public static void ArrangeWithOneShop(this DatabaseTestHelpers database, Shop shop)
        {
            database.Arrange.InsertOne(Tables.Shops, new
            {
                shop_id = shop.Id.ToGuid(),
                name = shop.Name,
                slug = shop.Slug.Value,
                created = DateTime.UtcNow,
                version = 1
            });
        }
    }
}