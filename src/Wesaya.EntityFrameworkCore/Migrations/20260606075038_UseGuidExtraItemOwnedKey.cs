using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wesaya.Migrations
{
    /// <inheritdoc />
    public partial class UseGuidExtraItemOwnedKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppMenuItemExtraItems",
                table: "AppMenuItemExtraItems");

            migrationBuilder.AddColumn<Guid>(
                name: "NewId",
                table: "AppMenuItemExtraItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppMenuItemExtraItems");

            migrationBuilder.RenameColumn(
                name: "NewId",
                table: "AppMenuItemExtraItems",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppMenuItemExtraItems",
                table: "AppMenuItemExtraItems",
                columns: new[] { "MenuItemId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppMenuItemExtraItems",
                table: "AppMenuItemExtraItems");

            migrationBuilder.AddColumn<int>(
                name: "NewId",
                table: "AppMenuItemExtraItems",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppMenuItemExtraItems");

            migrationBuilder.RenameColumn(
                name: "NewId",
                table: "AppMenuItemExtraItems",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppMenuItemExtraItems",
                table: "AppMenuItemExtraItems",
                columns: new[] { "MenuItemId", "Id" });
        }
    }
}
