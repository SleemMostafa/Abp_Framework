using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wesaya.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuItemExtraItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppMenuItemExtraItems",
                columns: table => new
                {
                    MenuItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMenuItemExtraItems", x => new { x.MenuItemId, x.Id });
                    table.ForeignKey(
                        name: "FK_AppMenuItemExtraItems_AppMenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "AppMenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuItemExtraItems_MenuItemId_Name",
                table: "AppMenuItemExtraItems",
                columns: new[] { "MenuItemId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppMenuItemExtraItems");
        }
    }
}
