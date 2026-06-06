using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wesaya.Migrations
{
    /// <inheritdoc />
    public partial class LocalizeMenuText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppMenuItemExtraItems_MenuItemId_Name",
                table: "AppMenuItemExtraItems");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AppMenuItems",
                newName: "NameEnglish");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "AppMenuItems",
                newName: "DescriptionEnglish");

            migrationBuilder.RenameIndex(
                name: "IX_AppMenuItems_Name",
                table: "AppMenuItems",
                newName: "IX_AppMenuItems_NameEnglish");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AppMenuItemExtraItems",
                newName: "NameEnglish");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AppMenuCategories",
                newName: "NameEnglish");

            migrationBuilder.RenameIndex(
                name: "IX_AppMenuCategories_Name",
                table: "AppMenuCategories",
                newName: "IX_AppMenuCategories_NameEnglish");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionArabic",
                table: "AppMenuItems",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameArabic",
                table: "AppMenuItems",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameArabic",
                table: "AppMenuItemExtraItems",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameArabic",
                table: "AppMenuCategories",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE AppMenuItems SET NameArabic = NameEnglish WHERE NameArabic = ''");
            migrationBuilder.Sql("UPDATE AppMenuItems SET DescriptionArabic = DescriptionEnglish WHERE DescriptionArabic IS NULL AND DescriptionEnglish IS NOT NULL");
            migrationBuilder.Sql("UPDATE AppMenuItemExtraItems SET NameArabic = NameEnglish WHERE NameArabic = ''");
            migrationBuilder.Sql("UPDATE AppMenuCategories SET NameArabic = NameEnglish WHERE NameArabic = ''");

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuItems_NameArabic",
                table: "AppMenuItems",
                column: "NameArabic");

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuCategories_NameArabic",
                table: "AppMenuCategories",
                column: "NameArabic");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppMenuItems_NameArabic",
                table: "AppMenuItems");

            migrationBuilder.DropIndex(
                name: "IX_AppMenuCategories_NameArabic",
                table: "AppMenuCategories");

            migrationBuilder.DropColumn(
                name: "DescriptionArabic",
                table: "AppMenuItems");

            migrationBuilder.DropColumn(
                name: "NameArabic",
                table: "AppMenuItems");

            migrationBuilder.DropColumn(
                name: "NameArabic",
                table: "AppMenuItemExtraItems");

            migrationBuilder.DropColumn(
                name: "NameArabic",
                table: "AppMenuCategories");

            migrationBuilder.RenameColumn(
                name: "NameEnglish",
                table: "AppMenuItems",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DescriptionEnglish",
                table: "AppMenuItems",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_AppMenuItems_NameEnglish",
                table: "AppMenuItems",
                newName: "IX_AppMenuItems_Name");

            migrationBuilder.RenameColumn(
                name: "NameEnglish",
                table: "AppMenuItemExtraItems",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameEnglish",
                table: "AppMenuCategories",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_AppMenuCategories_NameEnglish",
                table: "AppMenuCategories",
                newName: "IX_AppMenuCategories_Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppMenuItemExtraItems_MenuItemId_Name",
                table: "AppMenuItemExtraItems",
                columns: new[] { "MenuItemId", "Name" },
                unique: true);
        }
    }
}
