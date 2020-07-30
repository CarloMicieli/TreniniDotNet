using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TreniniDotNet.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    brand_id = table.Column<Guid>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    last_modified = table.Column<DateTime>(nullable: true),
                    version = table.Column<int>(nullable: false),
                    slug = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    website_url = table.Column<string>(maxLength: 255, nullable: true),
                    mail_address = table.Column<string>(maxLength: 255, nullable: true),
                    company_name = table.Column<string>(maxLength: 100, nullable: true),
                    group_name = table.Column<string>(maxLength: 100, nullable: true),
                    description = table.Column<string>(maxLength: 4000, nullable: true),
                    address_line1 = table.Column<string>(maxLength: 255, nullable: true),
                    address_line2 = table.Column<string>(maxLength: 255, nullable: true),
                    address_city = table.Column<string>(maxLength: 50, nullable: true),
                    address_region = table.Column<string>(maxLength: 50, nullable: true),
                    address_postal_code = table.Column<string>(maxLength: 10, nullable: true),
                    address_country = table.Column<string>(maxLength: 2, nullable: true),
                    kind = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brands", x => x.brand_id);
                });

            migrationBuilder.CreateTable(
                name: "collections",
                columns: table => new
                {
                    collection_id = table.Column<Guid>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    last_modified = table.Column<DateTime>(nullable: true),
                    version = table.Column<int>(nullable: false),
                    owner = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collections", x => x.collection_id);
                });

            migrationBuilder.CreateTable(
                name: "railways",
                columns: table => new
                {
                    railway_id = table.Column<Guid>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    last_modified = table.Column<DateTime>(nullable: true),
                    version = table.Column<int>(nullable: false),
                    slug = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    company_name = table.Column<string>(maxLength: 100, nullable: true),
                    country = table.Column<string>(maxLength: 2, nullable: false),
                    operating_since = table.Column<DateTime>(nullable: true),
                    operating_until = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: true),
                    gauge_in = table.Column<decimal>(nullable: true),
                    gauge_mm = table.Column<decimal>(nullable: true),
                    track_gauge = table.Column<string>(maxLength: 10, nullable: true),
                    total_length_km = table.Column<decimal>(nullable: true),
                    total_length_mi = table.Column<decimal>(nullable: true),
                    website_url = table.Column<string>(maxLength: 255, nullable: true),
                    headquarters = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_railways", x => x.railway_id);
                });

            migrationBuilder.CreateTable(
                name: "scales",
                columns: table => new
                {
                    scale_id = table.Column<Guid>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    last_modified = table.Column<DateTime>(nullable: true),
                    version = table.Column<int>(nullable: false),
                    slug = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    ratio = table.Column<decimal>(nullable: false),
                    gauge_in = table.Column<decimal>(nullable: true),
                    track_type = table.Column<string>(nullable: true),
                    description = table.Column<string>(maxLength: 4000, nullable: true),
                    weight = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scales", x => x.scale_id);
                });

            migrationBuilder.CreateTable(
                name: "shops",
                columns: table => new
                {
                    shop_id = table.Column<Guid>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    last_modified = table.Column<DateTime>(nullable: true),
                    version = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    slug = table.Column<string>(maxLength: 50, nullable: false),
                    website_url = table.Column<string>(maxLength: 100, nullable: true),
                    mail_address = table.Column<string>(maxLength: 100, nullable: true),
                    address_line1 = table.Column<string>(maxLength: 255, nullable: true),
                    address_line2 = table.Column<string>(maxLength: 255, nullable: true),
                    address_city = table.Column<string>(maxLength: 50, nullable: true),
                    address_region = table.Column<string>(maxLength: 50, nullable: true),
                    address_postal_code = table.Column<string>(maxLength: 10, nullable: true),
                    address_country = table.Column<string>(maxLength: 2, nullable: true),
                    phone_number = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shops", x => x.shop_id);
                });

            migrationBuilder.CreateTable(
                name: "wishlists",
                columns: table => new
                {
                    wishlist_id = table.Column<Guid>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    last_modified = table.Column<DateTime>(nullable: true),
                    version = table.Column<int>(nullable: false),
                    owner = table.Column<string>(maxLength: 50, nullable: false),
                    budget = table.Column<decimal>(nullable: true),
                    currency = table.Column<string>(maxLength: 3, nullable: true),
                    slug = table.Column<string>(maxLength: 50, nullable: false),
                    wishlist_name = table.Column<string>(maxLength: 100, nullable: true),
                    visibility = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishlists", x => x.wishlist_id);
                });

            migrationBuilder.CreateTable(
                name: "catalog_items",
                columns: table => new
                {
                    catalog_item_id = table.Column<Guid>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    last_modified = table.Column<DateTime>(nullable: true),
                    version = table.Column<int>(nullable: false),
                    brand_id = table.Column<Guid>(nullable: false),
                    slug = table.Column<string>(maxLength: 40, nullable: false),
                    item_number = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: false),
                    prototype_description = table.Column<string>(maxLength: 2500, nullable: true),
                    model_description = table.Column<string>(maxLength: 2500, nullable: true),
                    scale_id = table.Column<Guid>(nullable: false),
                    power_method = table.Column<string>(maxLength: 2, nullable: false),
                    delivery_date = table.Column<string>(maxLength: 10, nullable: true),
                    available = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalog_items", x => x.catalog_item_id);
                    table.ForeignKey(
                        name: "FK_catalog_items_brands_brand_id",
                        column: x => x.brand_id,
                        principalTable: "brands",
                        principalColumn: "brand_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catalog_items_scales_scale_id",
                        column: x => x.scale_id,
                        principalTable: "scales",
                        principalColumn: "scale_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "collection_items",
                columns: table => new
                {
                    collection_item_id = table.Column<Guid>(nullable: false),
                    catalog_item_id = table.Column<Guid>(nullable: false),
                    condition = table.Column<string>(maxLength: 15, nullable: false),
                    price = table.Column<decimal>(nullable: true),
                    currency = table.Column<string>(maxLength: 3, nullable: true),
                    PurchasedAtId = table.Column<Guid>(nullable: true),
                    added_date = table.Column<DateTime>(nullable: false),
                    removed_date = table.Column<DateTime>(nullable: true),
                    notes = table.Column<string>(maxLength: 150, nullable: true),
                    collection_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collection_items", x => x.collection_item_id);
                    table.ForeignKey(
                        name: "FK_collection_items_catalog_items_catalog_item_id",
                        column: x => x.catalog_item_id,
                        principalTable: "catalog_items",
                        principalColumn: "catalog_item_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_collection_items_collections_collection_id",
                        column: x => x.collection_id,
                        principalTable: "collections",
                        principalColumn: "collection_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_collection_items_shops_PurchasedAtId",
                        column: x => x.PurchasedAtId,
                        principalTable: "shops",
                        principalColumn: "shop_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rolling_stocks",
                columns: table => new
                {
                    rolling_stock_id = table.Column<Guid>(nullable: false),
                    railway_id = table.Column<Guid>(nullable: false),
                    category = table.Column<string>(maxLength: 25, nullable: false),
                    epoch = table.Column<string>(maxLength: 10, nullable: false),
                    min_radius = table.Column<decimal>(nullable: true),
                    couplers = table.Column<string>(maxLength: 25, nullable: true),
                    livery = table.Column<string>(maxLength: 50, nullable: true),
                    catalog_item_id = table.Column<Guid>(nullable: false),
                    rolling_stock_type = table.Column<string>(nullable: false),
                    type_name = table.Column<string>(maxLength: 15, nullable: true),
                    depot = table.Column<string>(maxLength: 100, nullable: true),
                    dcc_interface = table.Column<string>(maxLength: 10, nullable: true),
                    control = table.Column<string>(maxLength: 10, nullable: true),
                    passenger_car_type = table.Column<string>(maxLength: 25, nullable: true),
                    service_level = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolling_stocks", x => x.rolling_stock_id);
                    table.ForeignKey(
                        name: "FK_rolling_stocks_catalog_items_catalog_item_id",
                        column: x => x.catalog_item_id,
                        principalTable: "catalog_items",
                        principalColumn: "catalog_item_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rolling_stocks_railways_railway_id",
                        column: x => x.railway_id,
                        principalTable: "railways",
                        principalColumn: "railway_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wishlist_items",
                columns: table => new
                {
                    wishlist_item_id = table.Column<Guid>(nullable: false),
                    priority = table.Column<string>(maxLength: 10, nullable: false),
                    added_date = table.Column<DateTime>(nullable: false),
                    removed_date = table.Column<DateTime>(nullable: true),
                    price = table.Column<decimal>(nullable: true),
                    currency = table.Column<string>(maxLength: 3, nullable: true),
                    catalog_item_id = table.Column<Guid>(nullable: false),
                    notes = table.Column<string>(maxLength: 150, nullable: true),
                    wishlist_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishlist_items", x => x.wishlist_item_id);
                    table.ForeignKey(
                        name: "FK_wishlist_items_catalog_items_catalog_item_id",
                        column: x => x.catalog_item_id,
                        principalTable: "catalog_items",
                        principalColumn: "catalog_item_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wishlist_items_wishlists_wishlist_id",
                        column: x => x.wishlist_id,
                        principalTable: "wishlists",
                        principalColumn: "wishlist_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rolling_stocks1",
                columns: table => new
                {
                    RollingStockId = table.Column<Guid>(nullable: false),
                    length_in = table.Column<decimal>(nullable: false),
                    length_mm = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolling_stocks1", x => x.RollingStockId);
                    table.ForeignKey(
                        name: "FK_rolling_stocks1_rolling_stocks_RollingStockId",
                        column: x => x.RollingStockId,
                        principalTable: "rolling_stocks",
                        principalColumn: "rolling_stock_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rolling_stocks2",
                columns: table => new
                {
                    LocomotiveId = table.Column<Guid>(nullable: false),
                    class_name = table.Column<string>(maxLength: 15, nullable: false),
                    road_number = table.Column<string>(maxLength: 15, nullable: false),
                    series = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolling_stocks2", x => x.LocomotiveId);
                    table.ForeignKey(
                        name: "FK_rolling_stocks2_rolling_stocks_LocomotiveId",
                        column: x => x.LocomotiveId,
                        principalTable: "rolling_stocks",
                        principalColumn: "rolling_stock_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_brands_slug",
                table: "brands",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catalog_items_brand_id",
                table: "catalog_items",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_catalog_items_scale_id",
                table: "catalog_items",
                column: "scale_id");

            migrationBuilder.CreateIndex(
                name: "IX_catalog_items_slug",
                table: "catalog_items",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_collection_items_catalog_item_id",
                table: "collection_items",
                column: "catalog_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_collection_items_collection_id",
                table: "collection_items",
                column: "collection_id");

            migrationBuilder.CreateIndex(
                name: "IX_collection_items_PurchasedAtId",
                table: "collection_items",
                column: "PurchasedAtId");

            migrationBuilder.CreateIndex(
                name: "IX_collections_owner",
                table: "collections",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "IX_railways_slug",
                table: "railways",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rolling_stocks_catalog_item_id",
                table: "rolling_stocks",
                column: "catalog_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_rolling_stocks_railway_id",
                table: "rolling_stocks",
                column: "railway_id");

            migrationBuilder.CreateIndex(
                name: "IX_scales_slug",
                table: "scales",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_wishlist_items_catalog_item_id",
                table: "wishlist_items",
                column: "catalog_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_wishlist_items_wishlist_id",
                table: "wishlist_items",
                column: "wishlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_wishlists_owner",
                table: "wishlists",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "IX_wishlists_slug",
                table: "wishlists",
                column: "slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "collection_items");

            migrationBuilder.DropTable(
                name: "rolling_stocks1");

            migrationBuilder.DropTable(
                name: "rolling_stocks2");

            migrationBuilder.DropTable(
                name: "wishlist_items");

            migrationBuilder.DropTable(
                name: "collections");

            migrationBuilder.DropTable(
                name: "shops");

            migrationBuilder.DropTable(
                name: "rolling_stocks");

            migrationBuilder.DropTable(
                name: "wishlists");

            migrationBuilder.DropTable(
                name: "catalog_items");

            migrationBuilder.DropTable(
                name: "railways");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropTable(
                name: "scales");
        }
    }
}
