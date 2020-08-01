using FluentMigrator;

namespace TreniniDotNet.Infrastructure.Persistence.Migrations
{
    [Migration(20200315000000)]
    public sealed class InitialMigration : Migration
    {
        private const string RollingStocks = "rolling_stocks";
        private const string CatalogItems = "catalog_items";
        private const string CatalogItemsImages = "catalog_items_images";
        private const string Railways = "railways";
        private const string Brands = "brands";
        private const string Scales = "scales";
        private const string Collections = "collections";
        private const string CollectionItems = "collection_items";
        private const string Wishlists = "wishlists";
        private const string WishlistItems = "wishlist_items";
        private const string Shops = "shops";
        private const string Images = "images";

        public override void Up()
        {
            Create.Table(Images)
                .WithColumn("filename").AsString(15).PrimaryKey()
                .WithColumn("content_type").AsString(25).NotNullable()
                .WithColumn("content").AsBinary(512).NotNullable()
                .WithColumn("is_deleted").AsBoolean().Nullable()
                .WithColumn("created").AsDateTime().NotNullable();

            Create.Table(Brands)
                .WithColumn("brand_id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString(25).NotNullable()
                .WithColumn("slug").AsString(25).NotNullable()
                .WithColumn("brand_logo").AsString(15).Nullable()
                .WithColumn("company_name").AsString(100).Nullable()
                .WithColumn("group_name").AsString(100).Nullable()
                .WithColumn("description").AsString(1000).Nullable()
                .WithColumn("mail_address").AsString(100).Nullable()
                .WithColumn("website_url").AsString(100).Nullable()
                .WithColumn("kind").AsString(25).NotNullable()
                .WithColumn("address_line1").AsString(255).Nullable()
                .WithColumn("address_line2").AsString(255).Nullable()
                .WithColumn("address_city").AsString(50).Nullable()
                .WithColumn("address_region").AsString(50).Nullable()
                .WithColumn("address_postal_code").AsString(10).Nullable()
                .WithColumn("address_country").AsString(2).Nullable()
                .WithColumn("created").AsDateTime().NotNullable()
                .WithColumn("last_modified").AsDateTime().Nullable()
                .WithColumn("version").AsInt32().WithDefaultValue(1);

            Create.Index("Idx_Brands_Slug")
                .OnTable(Brands)
                .OnColumn("slug")
                .Unique();
            Create.Index("Idx_Brands_Name")
                .OnTable(Brands)
                .OnColumn("name")
                .Unique();

            Create.Table(Scales)
                .WithColumn("scale_id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString(25).NotNullable()
                .WithColumn("slug").AsString(25).NotNullable()
                .WithColumn("ratio").AsInt32().NotNullable()
                .WithColumn("gauge_mm").AsDecimal().NotNullable()
                .WithColumn("gauge_in").AsDecimal().NotNullable()
                .WithColumn("track_type").AsString(25).NotNullable()
                .WithColumn("description").AsString(250).Nullable()
                .WithColumn("weight").AsInt32().Nullable()
                .WithColumn("created").AsDateTime().NotNullable()
                .WithColumn("last_modified").AsDateTime().Nullable()
                .WithColumn("version").AsInt32().WithDefaultValue(1);

            Create.Index("Idx_Scales_Name")
                .OnTable(Scales)
                .OnColumn("name")
                .Unique();
            Create.Index("Idx_Scales_Slug")
                .OnTable(Scales)
                .OnColumn("slug")
                .Unique();

            Create.Table(Railways)
                .WithColumn("railway_id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString(25).NotNullable()
                .WithColumn("company_name").AsString(250).Nullable()
                .WithColumn("slug").AsString(25).NotNullable()
                .WithColumn("railway_logo").AsString(15).Nullable()
                .WithColumn("country").AsString(2).Nullable()
                .WithColumn("operating_since").AsDateTime().Nullable()
                .WithColumn("operating_until").AsDateTime().Nullable()
                .WithColumn("active").AsBoolean().Nullable()
                .WithColumn("gauge_mm").AsDecimal().Nullable()
                .WithColumn("gauge_in").AsDecimal().Nullable()
                .WithColumn("track_gauge").AsString(25).Nullable()
                .WithColumn("headquarters").AsString(250).Nullable()
                .WithColumn("total_length_mi").AsDecimal().Nullable()
                .WithColumn("total_length_km").AsDecimal().Nullable()
                .WithColumn("website_url").AsString(255).Nullable()
                .WithColumn("created").AsDateTime().NotNullable()
                .WithColumn("last_modified").AsDateTime().Nullable()
                .WithColumn("version").AsInt32().WithDefaultValue(1);

            Create.Index("Idx_Railways_Name")
                .OnTable(Railways)
                .OnColumn("name")
                .Unique();

            Create.Index("Idx_Railways_Slug")
                .OnTable(Railways)
                .OnColumn("slug")
                .Unique();

            #region [ Catalog items ]
            Create.Table(CatalogItems)
                .WithColumn("catalog_item_id").AsGuid().PrimaryKey()
                .WithColumn("brand_id").AsGuid().NotNullable()
                .WithColumn("scale_id").AsGuid().NotNullable()
                .WithColumn("item_number").AsString(10).NotNullable()
                .WithColumn("slug").AsString(40).NotNullable()
                .WithColumn("power_method").AsString(2).NotNullable()
                .WithColumn("delivery_date").AsString(10).Nullable()
                .WithColumn("available").AsBoolean().Nullable()
                .WithColumn("description").AsString(250).NotNullable()
                .WithColumn("model_description").AsString(2500).Nullable()
                .WithColumn("prototype_description").AsString(2500).Nullable()
                .WithColumn("created").AsDateTime().NotNullable()
                .WithColumn("last_modified").AsDateTime().Nullable()
                .WithColumn("version").AsInt32().WithDefaultValue(1);

            Create.Index("Idx_CatalogItems_Slug")
                .OnTable(CatalogItems)
                .OnColumn("slug")
                .Unique();

            Create.Index("Idx_CatalogItems_BrandId_ItemNumber")
                .OnTable(CatalogItems)
                .OnColumn("item_number").Ascending()
                .OnColumn("brand_id").Ascending()
                .WithOptions().Unique();

            Create.ForeignKey("FK_CatalogItems_Brands")
                .FromTable(CatalogItems).ForeignColumn("brand_id")
                .ToTable(Brands).PrimaryColumn("brand_id");

            Create.ForeignKey("FK_CatalogItems_Scales")
                .FromTable(CatalogItems).ForeignColumn("scale_id")
                .ToTable(Scales).PrimaryColumn("scale_id");

            Create.Table(RollingStocks)
                .WithColumn("rolling_stock_id").AsGuid().PrimaryKey()
                .WithColumn("era").AsString(10).NotNullable()
                .WithColumn("category").AsString(25).NotNullable()
                .WithColumn("railway_id").AsGuid().NotNullable()
                .WithColumn("catalog_item_id").AsGuid().NotNullable()
                .WithColumn("length_mm").AsDecimal().Nullable()
                .WithColumn("length_in").AsDecimal().Nullable()
                .WithColumn("class_name").AsString(25).Nullable()
                .WithColumn("road_number").AsString(25).Nullable()
                .WithColumn("type_name").AsString(25).Nullable()
                .WithColumn("couplers").AsString(10).Nullable()
                .WithColumn("livery").AsString(50).Nullable()
                .WithColumn("dcc_interface").AsString(25).Nullable()
                .WithColumn("passenger_car_type").AsString(25).Nullable()
                .WithColumn("service_level").AsString(15).Nullable()
                .WithColumn("control").AsString(25).Nullable()
                .WithColumn("min_radius").AsDecimal().Nullable();

            Create.ForeignKey("FK_RollingStocks_Railways")
                .FromTable(RollingStocks).ForeignColumn("railway_id")
                .ToTable(Railways).PrimaryColumn("railway_id");

            Create.ForeignKey("FK_RollingStocks_CatalogItems")
                .FromTable(RollingStocks).ForeignColumn("catalog_item_id")
                .ToTable(CatalogItems).PrimaryColumn("catalog_item_id");

            Create.Table(CatalogItemsImages)
                .WithColumn("catalog_item_id").AsGuid().NotNullable()
                .WithColumn("filename").AsString(15).NotNullable()
                .WithColumn("is_default").AsBoolean().Nullable();

            Create.PrimaryKey()
                .OnTable(CatalogItemsImages)
                .Columns("catalog_item_id", "filename");

            Create.ForeignKey("FK_CatalogItemsImages_Images")
                .FromTable(CatalogItemsImages).ForeignColumn("filename")
                .ToTable(Images).PrimaryColumn("filename");

            Create.ForeignKey("FK_CatalogItemsImages_CatalogItems")
                .FromTable(CatalogItemsImages).ForeignColumn("catalog_item_id")
                .ToTable(CatalogItems).PrimaryColumn("catalog_item_id");
            #endregion

            #region [ Shops ]

            Create.Table(Shops)
                .WithColumn("shop_id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString(50).NotNullable()
                .WithColumn("slug").AsString(50).NotNullable()
                .WithColumn("website_url").AsString(100).Nullable()
                .WithColumn("phone_number").AsString(50).Nullable()
                .WithColumn("mail_address").AsString(100).Nullable()
                .WithColumn("address_line1").AsString(255).Nullable()
                .WithColumn("address_line2").AsString(255).Nullable()
                .WithColumn("address_city").AsString(50).Nullable()
                .WithColumn("address_region").AsString(50).Nullable()
                .WithColumn("address_postal_code").AsString(10).Nullable()
                .WithColumn("address_country").AsString(2).Nullable()
                .WithColumn("created").AsDateTime().NotNullable()
                .WithColumn("last_modified").AsDateTime().Nullable()
                .WithColumn("version").AsInt32().WithDefaultValue(1);

            Create.Index("Idx_Shops_Slug")
                .OnTable(Shops)
                .OnColumn("slug");

            #endregion

            #region [ Collections ]

            Create.Table(Collections)
                .WithColumn("collection_id").AsGuid().PrimaryKey()
                .WithColumn("owner").AsString(50).NotNullable()
                .WithColumn("created").AsDateTime().NotNullable()
                .WithColumn("last_modified").AsDateTime().Nullable()
                .WithColumn("version").AsInt32().WithDefaultValue(1);

            Create.Index("Idx_Collections_Owner")
                .OnTable(Collections)
                .OnColumn("owner");

            Create.Table(CollectionItems)
                .WithColumn("item_id").AsGuid().PrimaryKey()
                .WithColumn("collection_id").AsGuid().NotNullable()
                .WithColumn("catalog_item_id").AsGuid().NotNullable()
                .WithColumn("catalog_item_slug").AsString(40).NotNullable()
                .WithColumn("condition").AsString(15).NotNullable()
                .WithColumn("price").AsDecimal().NotNullable()
                .WithColumn("currency").AsString(3).NotNullable()
                .WithColumn("shop_id").AsGuid().Nullable()
                .WithColumn("added_date").AsDateTime().Nullable()
                .WithColumn("removed_date").AsDateTime().Nullable()
                .WithColumn("notes").AsString(150).Nullable();

            Create.ForeignKey("FK_CollectionItems_Collections")
                .FromTable(CollectionItems).ForeignColumn("collection_id")
                .ToTable(Collections).PrimaryColumn("collection_id");

            Create.ForeignKey("FK_CollectionItems_CatalogItems")
                .FromTable(CollectionItems).ForeignColumn("catalog_item_id")
                .ToTable(CatalogItems).PrimaryColumn("catalog_item_id");

            Create.ForeignKey("FK_CollectionItems_Shops")
                .FromTable(CollectionItems).ForeignColumn("shop_id")
                .ToTable(Shops).PrimaryColumn("shop_id");

            #endregion

            #region [ Wishlists ]

            Create.Table(Wishlists)
                .WithColumn("wishlist_id").AsGuid().PrimaryKey()
                .WithColumn("owner").AsString(50).NotNullable()
                .WithColumn("slug").AsString(100).NotNullable()
                .WithColumn("wishlist_name").AsString(100).Nullable()
                .WithColumn("visibility").AsString(15).NotNullable()
                .WithColumn("created").AsDateTime().NotNullable()
                .WithColumn("last_modified").AsDateTime().Nullable()
                .WithColumn("version").AsInt32().WithDefaultValue(1);

            Create.Index("Idx_Wishlists_Slug")
                .OnTable(Wishlists)
                .OnColumn("slug");

            Create.Table(WishlistItems)
                .WithColumn("item_id").AsGuid().PrimaryKey()
                .WithColumn("wishlist_id").AsGuid().NotNullable()
                .WithColumn("catalog_item_id").AsGuid().NotNullable()
                .WithColumn("catalog_item_slug").AsString(40).NotNullable()
                .WithColumn("priority").AsString(10).NotNullable()
                .WithColumn("added_date").AsDateTime().NotNullable()
                .WithColumn("removed_date").AsDateTime().Nullable()
                .WithColumn("price").AsDecimal().Nullable()
                .WithColumn("currency").AsString(3).Nullable()
                .WithColumn("notes").AsString(150).Nullable();

            Create.ForeignKey("FK_WishlistItems_Wishlists")
                .FromTable(WishlistItems).ForeignColumn("wishlist_id")
                .ToTable(Wishlists).PrimaryColumn("wishlist_id");

            Create.ForeignKey("FK_WishlistItems_CatalogItems")
                .FromTable(WishlistItems).ForeignColumn("catalog_item_id")
                .ToTable(CatalogItems).PrimaryColumn("catalog_item_id");

            #endregion
        }

        public override void Down()
        {
            Delete.Table(WishlistItems);
            Delete.Table(Wishlists);
            Delete.Table(CollectionItems);
            Delete.Table(Collections);
            Delete.Table(Shops);
            Delete.Table(CatalogItemsImages);
            Delete.Table(RollingStocks);
            Delete.Table(CatalogItems);
            Delete.Table(Scales);
            Delete.Table(Brands);
            Delete.Table(Railways);
            Delete.Table(Images);
        }
    }
}
