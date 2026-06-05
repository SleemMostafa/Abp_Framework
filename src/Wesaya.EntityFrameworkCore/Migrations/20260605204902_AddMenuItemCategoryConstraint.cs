using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wesaya.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuItemCategoryConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_AppMenuItems_AppMenuCategories_CategoryId",
                table: "AppMenuItems",
                column: "CategoryId",
                principalTable: "AppMenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppMenuItems_AppMenuCategories_CategoryId",
                table: "AppMenuItems");
        }
    }
}
