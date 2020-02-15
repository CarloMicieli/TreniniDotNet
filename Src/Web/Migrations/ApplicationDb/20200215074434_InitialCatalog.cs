using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TreniniDotNet.Web.Migrations.ApplicationDb
{
    public partial class InitialCatalog : Migration
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
                    Kind = table.Column<string>(maxLength: 25, nullable: false)
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
                    OperatingSince = table.Column<DateTime>(nullable: true)
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
                    Ratio = table.Column<float>(nullable: false),
                    Gauge = table.Column<decimal>(nullable: false),
                    TrackGauge = table.Column<string>(maxLength: 15, nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scales", x => x.ScaleId);
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
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Railways");

            migrationBuilder.DropTable(
                name: "Scales");
        }
    }
}
