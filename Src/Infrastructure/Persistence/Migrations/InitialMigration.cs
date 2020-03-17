﻿using FluentMigrator;

namespace TreniniDotNet.Infrastracture.Persistence.Migrations
{
    [Migration(20200315000000)]
    public sealed class InitialMigration : Migration
    {
        private const string RollingStocks = "rolling_stocks";
        private const string CatalogItems = "catalog_items";
        private const string Railways = "railways";
        private const string Brands = "brands";
        private const string Scales = "scales";

        public override void Up()
        {
            Create.Table(Brands)
                .WithColumn("brand_id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString(25).NotNullable()
                .WithColumn("slug").AsString(25).NotNullable()
                .WithColumn("company_name").AsString(100).Nullable()
                .WithColumn("description").AsString(1000).Nullable()
                .WithColumn("mail_address").AsString(255).Nullable()
                .WithColumn("website_url").AsString(255).Nullable()
                .WithColumn("kind").AsString(25).Nullable()
                .WithColumn("created_at").AsDateTime().Nullable()
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
                .WithColumn("ratio").AsDecimal().NotNullable()
                .WithColumn("gauge").AsDecimal().NotNullable()
                .WithColumn("track_type").AsString(25).NotNullable()
                .WithColumn("notes").AsString(250).Nullable()
                .WithColumn("created_at").AsDateTime().Nullable()
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
                .WithColumn("country").AsString(2).Nullable()
                .WithColumn("operating_since").AsDateTime().Nullable()
                .WithColumn("operating_until").AsDateTime().Nullable()
                .WithColumn("active").AsBoolean().Nullable()
                .WithColumn("created_at").AsDateTime().Nullable()
                .WithColumn("version").AsInt32().WithDefaultValue(1);

            Create.Index("Idx_Railways_Name")
                .OnTable(Railways)
                .OnColumn("name")
                .Unique();

            Create.Index("Idx_Railways_Slug")
                .OnTable(Railways)
                .OnColumn("slug")
                .Unique();

            Create.Table(CatalogItems)
                .WithColumn("catalog_item_id").AsGuid().PrimaryKey()
                .WithColumn("brand_id").AsGuid().NotNullable()
                .WithColumn("scale_id").AsGuid().NotNullable()
                .WithColumn("item_number").AsString(10).NotNullable()
                .WithColumn("slug").AsString(40).NotNullable()
                .WithColumn("power_method").AsString(2).NotNullable()
                .WithColumn("delivery_date").AsString(10).Nullable()
                .WithColumn("description").AsString(250).NotNullable()
                .WithColumn("model_description").AsString(2500).Nullable()
                .WithColumn("prototype_description").AsString(2500).Nullable()
                .WithColumn("created_at").AsDateTime().Nullable()
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
                .WithColumn("era").AsString(5).NotNullable()
                .WithColumn("category").AsString(25).NotNullable()
                .WithColumn("railway_id").AsGuid().NotNullable()
                .WithColumn("catalog_item_id").AsGuid().NotNullable()
                .WithColumn("length").AsDecimal().Nullable()
                .WithColumn("class_name").AsString(50).Nullable()
                .WithColumn("road_number").AsString(50).Nullable();

            Create.ForeignKey("FK_RollingStocks_Railways")
                .FromTable(RollingStocks).ForeignColumn("railway_id")
                .ToTable(Railways).PrimaryColumn("railway_id");

            Create.ForeignKey("FK_RollingStocks_CatalogItems")
                .FromTable(RollingStocks).ForeignColumn("catalog_item_id")
                .ToTable(CatalogItems).PrimaryColumn("catalog_item_id");
        }

        public override void Down()
        {
            Delete.Table(RollingStocks);
            Delete.Table(CatalogItems);
            Delete.Table(Scales);
            Delete.Table(Brands);
            Delete.Table(Railways);
        }
    }
}