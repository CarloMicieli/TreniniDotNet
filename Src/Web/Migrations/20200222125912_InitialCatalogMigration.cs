using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TreniniDotNet.Web.Migrations
{
    public partial class InitialCatalogMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<Guid>(nullable: false),
                    Slug = table.Column<string>(maxLength: 25, nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    WebsiteUrl = table.Column<string>(maxLength: 250, nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 250, nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    BrandKind = table.Column<string>(maxLength: 25, nullable: true),
                    Active = table.Column<bool>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Railways",
                columns: table => new
                {
                    RailwayId = table.Column<Guid>(nullable: false),
                    Slug = table.Column<string>(maxLength: 25, nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Status = table.Column<string>(maxLength: 10, nullable: true),
                    OperatingUntil = table.Column<DateTime>(nullable: true),
                    OperatingSince = table.Column<DateTime>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Railways", x => x.RailwayId);
                });

            migrationBuilder.CreateTable(
                name: "Scales",
                columns: table => new
                {
                    ScaleId = table.Column<Guid>(nullable: false),
                    Slug = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    Ratio = table.Column<decimal>(nullable: false),
                    Gauge = table.Column<decimal>(nullable: false),
                    TrackGauge = table.Column<string>(maxLength: 15, nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scales", x => x.ScaleId);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    CatalogItemId = table.Column<Guid>(nullable: false),
                    BrandId = table.Column<Guid>(nullable: false),
                    Slug = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Category = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    ScaleId = table.Column<Guid>(nullable: false),
                    ItemNumber = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    PrototypeDescription = table.Column<string>(maxLength: 2500, nullable: true),
                    ModelDescription = table.Column<string>(maxLength: 2500, nullable: true),
                    DeliveryDate = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DirectCurrent = table.Column<bool>(nullable: true),
                    Released = table.Column<bool>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.CatalogItemId);
                    table.ForeignKey(
                        name: "FK_CatalogItems_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogItems_Scales_ScaleId",
                        column: x => x.ScaleId,
                        principalTable: "Scales",
                        principalColumn: "ScaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RollingStock",
                columns: table => new
                {
                    RollingStockId = table.Column<Guid>(nullable: false),
                    Era = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    Length = table.Column<decimal>(nullable: true),
                    RailwayId = table.Column<Guid>(nullable: false),
                    RoadName = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    RoadNumber = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Livery = table.Column<string>(maxLength: 50, nullable: true),
                    Series = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    DepotName = table.Column<string>(maxLength: 50, nullable: true),
                    CatalogItemId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RollingStock", x => x.RollingStockId);
                    table.ForeignKey(
                        name: "FK_RollingStock_CatalogItems_CatalogItemId",
                        column: x => x.CatalogItemId,
                        principalTable: "CatalogItems",
                        principalColumn: "CatalogItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RollingStock_Railways_RailwayId",
                        column: x => x.RailwayId,
                        principalTable: "Railways",
                        principalColumn: "RailwayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "Idx_Brands_Name",
                table: "Brands",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Idx_Brands_Slug",
                table: "Brands",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_BrandId",
                table: "CatalogItems",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "Idx_CatalogItems_ItemNumber_BrandName",
                table: "CatalogItems",
                column: "ItemNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_ScaleId",
                table: "CatalogItems",
                column: "ScaleId");

            migrationBuilder.CreateIndex(
                name: "Idx_CatalogItems_Slug",
                table: "CatalogItems",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Idx_Railways_Name",
                table: "Railways",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Idx_Railways_Slug",
                table: "Railways",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RollingStock_CatalogItemId",
                table: "RollingStock",
                column: "CatalogItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RollingStock_RailwayId",
                table: "RollingStock",
                column: "RailwayId");

            migrationBuilder.CreateIndex(
                name: "Idx_Scales_Name",
                table: "Scales",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Idx_Scales_Slug",
                table: "Scales",
                column: "Slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RollingStock");

            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.DropTable(
                name: "Railways");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Scales");
        }
    }
}
