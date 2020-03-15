using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TreniniDotNet.Web.Migrations
{
    public partial class CatalogItemsChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RollingStock_CatalogItems_CatalogItemId",
                table: "RollingStock");

            migrationBuilder.DropColumn(
                name: "RoadName",
                table: "RollingStock");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "DirectCurrent",
                table: "CatalogItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "CatalogItemId",
                table: "RollingStock",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "RollingStock",
                unicode: false,
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "RollingStock",
                unicode: false,
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PowerMethod",
                table: "CatalogItems",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RollingStock_CatalogItems_CatalogItemId",
                table: "RollingStock",
                column: "CatalogItemId",
                principalTable: "CatalogItems",
                principalColumn: "CatalogItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RollingStock_CatalogItems_CatalogItemId",
                table: "RollingStock");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "RollingStock");

            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "RollingStock");

            migrationBuilder.DropColumn(
                name: "PowerMethod",
                table: "CatalogItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "CatalogItemId",
                table: "RollingStock",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "RoadName",
                table: "RollingStock",
                type: "character varying(25)",
                unicode: false,
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "CatalogItems",
                type: "character varying(25)",
                unicode: false,
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "DirectCurrent",
                table: "CatalogItems",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RollingStock_CatalogItems_CatalogItemId",
                table: "RollingStock",
                column: "CatalogItemId",
                principalTable: "CatalogItems",
                principalColumn: "CatalogItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
